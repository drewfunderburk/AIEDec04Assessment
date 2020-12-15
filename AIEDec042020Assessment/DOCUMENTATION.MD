# AIEDec04Assessment Documentation

This tech demo uses Raylib_cs and MathLibrary extensively.
___

### Table of Contents
- [class Program](#class-program)
- [class Game](#class-game)
- [class Actor](#class-actor)
  - [enum ActorID](#enum-actorid)
  - [class Player](#class-player-actor)
  - [class Bullet](#class-bullet-actor)
  - [class Enemy](#class-enemy-actor)
    - [class PatrolEnemy](#class-patrolenemy-enemy)
    - [class HomingEnemy](#class-homingenemy-enemy)
  - [class Collider](#class-collider-actor)
    - [class CircleCollider](#class-circlecollider-collider)
- [class Scene](#class-scene)
  - [class Level](#class-level-scene)
    - [class Level1](#class-level1-level)
      - [struct Wave](#struct-wave)
- [class Sprite](#class-sprite)
___

### class Program
Main Driver for the game. Used only to create the game and run it.

Variable | Use
:--------|:----
private Game game | Starts a new instance of Game

Function | Parameters | Use
:--------|:-----------|:----
private static void Main | string[] args | Main function of the program. Used to run the game
___

### class Game
Main game class. Contains the game loop and is responsible for managing everything that goes on in the game.

Variable | Use
:--------|:----
private static Scene[] _scenes       | Stores all scenes used in the game
public static bool GameOver          | Whether or not the game should end
public static int CurrentSceneIndex  | Index of the current scene

Function | Parameters | Use
:--------|:-----------|:----
**CORE**                            | -               | -
private void Start                  | N/A             | Runs when the game starts
private void Update                 | float deltaTime | Runs every frame. Used for game logic
private void Draw                   | N/A             | Runs every frame. Used for drawing to the screen
private void End                    | N/A             | Runs when the game ends
public void Run                     | N/A             | Game Loop 
**SCENE**                           | -               | -
public static Scene GetScene        | int index       | Retrieve a scene
public static Scene GetCurrentScene | N/A             | Get the current scene
public static int AddScene          | Scene scene     | Add a scene to the game
public static bool RemoveScene      | Scene scene     | Remove a scene from the game
public static void SetCurrentScene  | int index       | Set the current scene
___

### class Actor
Base class for all things that physically exist.
<br><br>
> ##### enum ActorID
> Public enum used in collision to know what type of actor you've collided with
>
> |Options
> |:------
> |PLAYER
> |ENEMY
> |PLAYER_BULLET
> |ENEMY_BULLET

<br>

Variable | Use
:--------|:----
public bool Started               | Whether or not the actor is started
public bool WillDestroy           | Whether or not the actor will be destroyed on the next update
public Sprite _sprite             | The actor's sprite
public Actor _parent              | The actor's parent
private Actor[] _children         | The actor's children
private Matrix3 _globalTransform  | The actor's global transform matrix
private Matrix3 _localTransform   | The actor's local transform matrix
private Matrix3 _translation      | The actor's local translation matrix
private Matrix3 _rotation         | The actor's local rotation matrix
private Matrix3 _scale            | The actor's local scale matrix
public List\<Collider> _colliders | The actor's colliders
public ActorID ID                 | The actor's ID
public Vector2 Scale              | Quick reference to the actor's local scale
public float RotationAngle        | Quick reference to the actor's local rotation angle in radians
public float Speed                | The actor's rate of acceleration
public float MaxSpeed             | The actor's top speed
public Vector2 Forward            | The actor's forward vector. (X-Axis forward)
public Vector2 GlobalPosition     | The actor's position relative to global 0, 0
public Vector2 LocalPosition      | The actor's position relative to it's parent
public Vector2 Velocity           | The actor's current velocity
protected Vector2 Acceleration    | The actor's current acceleration

Function | Parameters | Use
:--------|:-----------|:----
**CORE**                             | -                                      | -
public virtual void Start            | N/A                                    | Called when this actor is started
public virtual void Update           | float deltaTime                        | Called every frame. Updates actor logic
public virtual void Draw             | N/A                                    | Called every frame. Draws actor to the screen
public virtual void End              | N/A                                    | Called when an actor is destroyed
**CONSTRUCTOR**                      | -                                      | -
public Actor                         | Vector2 position,<br>float rotation    | Creates a new actor at the given position with the given rotation
public Actor                         | float x,<br>float y,<br>float rotation | Creates a new actor at the given position with the given rotation
**CHILDREN**                         | -                                      | -
public bool AddChild                 | Actor child                            | Adds a child to this actor
public bool RemoveChild              | Actor child                            | Removes a child from this actor
**UTILITY**                          | -                                      | -
public static Actor Instantiate      | Actor actor                            | Adds an actor to the scene
public static void Destroy           | Actor actor                            | Removes an actor from the scene
public void AddCollider              | Collider collider                      | Adds a collider to this actor
public void RemoveCollider           | Collider collider                      | Removes a collider from this actor
**TRANSFORMATION**                   | -                                      | -
public void SetTranslation           | Vector2 position                       | Sets the actor's local translation to the given position
public void SetRotation              | float radians                          | Sets the actor's local rotation to the given value
public void Rotate                   | float radians                          | Rotates the actor's local transform by the given value
public void SetScale                 | float x,<br>float y                    | Sets the actor's local scale to the given value
public void SetScale                 | Vector2 scale                          | Sets the actor's local scale to the given value
public void LookAt                   | Vector2 position                       | Rotates the actor to face a position
protected void UpdateGlobalTransform | N/A                                    | Updates the actor's global transform as well as it's children's
protected void UpdateTransform       | N/A                                    | Updates the actor's local transform and the global transforms of it's children
**COLLISION**                        | -                                      | -
public bool CheckCollision           | Actor other                            | Check if this actor has collided with another
public virtual bool OnCollision      | Actor other                            | Actions to perform if a collision is detected
___

### class Player : Actor
Class to define the player

Variable | Use
:--------|:----
private float _fireDelay                            | Delay between shots
private float _colliderRadius                       | Radius of colliders
private float _maxHealth                            | Maximum health
private float _health                               | Current health
private Vector2 _acceleration                       | Current acceleration
private float _leftBoundary                         | Left screen movement boundary
private float _rightBoundary                        | Right screen movement boundary
private float _bottomBoundary                       | Bottom screen movement boundary
private float _topBoundary                          | Top screen movement boundary
private System.Diagnostics.Stopwatch _fireRateTimer | Timer for use in fire rate
private Turret _leftTurret                          | Left player auto-turret. Will track and fire at the nearest enemy
private Turret _rightTurret                         | Right player auto-turret. Will track and fire at the nearest enemy

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**                  | -                                      | -
public Player                    | Vector2 position,<br>float rotation    | Creates a new player
public Player                    | float x,<br>float y,<br>float rotation | Creates a new player
**CORE**                         | -                                      | -
public override void Start       | N/A                                    | Called when a player is started
public override void Update      | float deltaTime                        | Called every frame. Updates player logic
public override bool OnCollision | Actor other                            | Actions to perform if a collision is detected
public void TakeDamage           | int damage                             | Causes the player to take damage
___

### class Turret : Actor
Variable | Use
:--------|:----
private float _fireDelay                            | Delay between shots
private System.Diagnostics.Stopwatch _fireRateTimer | Timer for use in fire rate

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**                  | -                                      | -
public Turret                    | Vector2 position,<br>float rotation    | Creates a new turret
**CORE**                         | -                                      | -
public override void Start       | N/A                                    | Called when this turret is started
public override void Update      | float deltaTime                        | Called every frame. Updates turret logic
public override bool OnCollision | Actor other                            | Actions to perform if a collision is detected
protected void Shoot             | N/A                                    | Attempt to fire a bullet
___

### class Bullet : Actor
Projectile to be fired

Variable | Use
:--------|:----
private System.Diagnostics.Stopwatch _timer | Timer used for auto-destroy
public float DespawnTime                    | Maximum time this bullet should live

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**                  | -                                                 | -
public Bullet                    | Vector2 position,<br>Vector2 scale,<br>ActorID ID | Creates a new Bullet
**CORE**                         | -                                                 | -
public override void Start       | N/A                                               | Called when this Bullet starts
public override void Update      | float deltaTime                                   | Called every frame. Updates Bullet logic
**MISC**                         | -                                                 | -
public override bool OnCollision | Actor other                                       | Actions to perform if a collision is detected
___

### class Enemy : Actor
Base class for all enemies

Variable | Use
:--------|:----
private float _fireDelay                    | Delay between shots
private System.Diagnostics.Stopwatch _timer | Fire delay timer
public Actor Target                         | Enemy will constantly LookAt this actor

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**                  | -                                      | -
public Enemy                     | Vector2 position,<br>float rotation    | Creates a new enemy
public Enemy                     | float x,<br>float y,<br>float rotation | Creates a new enemy
**CORE**                         | -                                      | -
public override void Start       | N/A                                    | Called when this enemy is started
public override void Update      | float deltaTime                        | Called every frame. Updates enemy logic
public override bool OnCollision | Actor other                            | Actions to perform if a collision is detected
protected void Shoot             | N/A                                    | Attempt to fire a bullet
___

### class PatrolEnemy : Enemy
Enemy that patrols a given distance back and forth on the X-Axis

Variable | Use
:--------|:----
public float PatrolDistance | Distance back and forth to patrol
public float PatrolPeriod   | Length of one full period of motion
private float _startingX    | The X position this enemy started at

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**             | -                                                                                   | -
public PatrolEnemy          | Vector2 position,<br>float rotation,<br>float patrolDistance,<br>float patrolPeriod | Creates a new PatrolEnemy
**CORE**                    | -                                                                                   | -
public override void Start  | N/A                                                                                 | Called when this PatrolEnemy is started
public override void Update | float deltaTime                                                                     | Called every frame. Updates PatrolEnemy logic
___

### class HomingEnemy : Enemy
Enemy that homes in on a target

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**             | -                                   | -
public HomingEnemy          | Vector2 position,<br>float rotation | Creates a new HomingEnemy
**CORE**                    |                                     | -
public override void Start  | N/A                                 | Called when this HomingEnemy is started
public override void Update | float deltaTime                     | Called every frame. Updates HomingEnemy logic
___

### class Collider : Actor
Abstract base class for all colliders

Function | Parameters | Use
:--------|:-----------|:----
public Collider                 | Vector2 position,<br>float rotation | Base constructor to satisfy Actor
public abstract bool IsCollided | Collider other                      | Is this collider collided with another
___

### class CircleCollider : Collider
Basic Circle collision

Variable | Use
:--------|:----
public float Radius | Radius of the collider

Function | Parameters | Use
:--------|:-----------|:----
public CircleCollider           | Vector2 position,<br>float radius | Creates a new CircleCollider
public override void Draw       | N/A                               | Draw the collision radius for debug purposes
public override bool IsCollided | Collider collidedActor            | Checks collision against another collider
___

### class Scene
Holds actors and objects in a contained space

Variable | Use
:--------|:----
public bool Started                         | Whether this scene has been started
public int NumActors                        | Number of actors in the scene
protected Actor[] _actors                   | Array of all actors in this scene
public Matrix3 _globalTransform             | The scene's global transform matrix
public Matrix3 _localTransform              | The scene's local transform matrix
private Matrix3 _translation                | The scene's local translation matrix
private Matrix3 _rotation                   | The scene's local rotation matrix
private Matrix3 _scale                      | The scene's local scale matrix
private int _shakeCounter                   | Used for camerashake to determine how many times we have moved the scene
private bool _cameraIsShaking               | Whether or not the camera is shaking
public bool CameraIsShaking                 | Property for _cameraIsShaking that ensures _timer is reset whenever this variable is set
private System.Diagnostics.Stopwatch _timer | Timer used for camera shake

Function | Parameters | Use
:--------|:-----------|:----
**CORE**                       | -                | -
public virtual void Start      | N/A              | Start the scene
public virtual void Update     | float deltaTime  | Update the scene and all actors contained in it
public virtual void Draw       | N/A              | Draw all actors to the screen
public virtual void End        | N/A              | End the scene and all actors contained in it
**ACTOR METHODS**              | -                | -
public void AddActor           | Actor actor      | Adds an actor to the scene
public void RemoveActor        | int index        | Removes an actor from the scene
public void RemoveActor        | Actor actor      | Removes an actor from the scene
public Actor GetActor          | int index        | Returns the actor at a specified index
private void DestroyActors     | N/A              | Removes all actors that are marked for removal
**TRANSFORMATION**             | -                | -
public void SetTranslation     | Vector2 position | Sets the scene's position
protected void UpdateTransform | N/A              | Updates the scene's transform
**MISC**                       | -                | -
private void ShakeCamera       | N/A              | Simulates camera shake by moving the scene to a series of random points before recentering it at the origin
___

### class Level : Scene
Base class for levels

Variable | Use
:--------|:----
protected Player _player                      | Reference to the player

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**                | -   | -
public Level                   | N/A | Creates a new Level
**CORE**                       | -   | -
public override void Start     | N/A | Called when this Level starts
public int GetRemainingEnemies | N/A | Called every frame. Updates Level logic
___

### class Level1 : Level
First level in the game

> ##### struct Wave
> Private struct to hold waves of enemies
> 
> Variable | Use
> :--------|:----
> public bool Spawned   | Whether or not this Wave has been spawned
> public int StartTime  | What time on the timer this wave is meant to start
> public int Delay      | Delay between enemy spawns
> public int MaxCounter | Maximum number of enemies to spawn
> public int Counter    | Number of enemies from this wave spawned
>
> Function | Parameters | Use
> :--------|:-----------|:----
> public Wave | bool spawned,<br>int startTime,<br>int delay,<br>int maxCounter,<br>int counter | Creates a new wave

Variable | Use
:--------|:----
private Wave[] _waves                       | Stores this level's waves
private System.Diagnostics.Stopwatch _timer | Timer for wave timings

Function | Parameters | Use
:--------|:-----------|:----
**CONSTRUCTOR**             | -               | -
public Level1               | N/A             | Creates a new Level1
**CORE**                    | -               | -
public override void Start  | N/A             | Called when this Level is started
public override void Update | float deltaTime | Called every frame. Updates Level1 logic
**MISC**                    | -               | -
private void SpawnWave      | int index       | Spawns a wave of enemies from _waves
___

### class Sprite
Specifies an image to be used in 2D space

Variable | Use
:--------|:----
private Texture2D _texture | Texture object
private float _scale       | Scale of all sprites
public float Scale         | Property for _scale. Multiplies by 32 when setting
public int Width           | Width of the loaded texture
public int Height          | Height of the loaded texture

Function | Parameters | Use
:--------|:-----------|:----
public Sprite    | Texture2D texture | Loads the given texture
public Sprite    | string path       | Loads the texture at the given path
public void Draw | Matrix3 transform | Draws the sprite using a given transform


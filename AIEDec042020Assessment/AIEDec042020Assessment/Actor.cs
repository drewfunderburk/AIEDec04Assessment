using System;
using System.Collections.Generic;
using System.Text;

namespace AIEDec042020Assessment
{
    class Actor
    {
        public bool Started { get; set; }
        public bool WillDestroy { get; set; }

        #region CORE
        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void End()
        {

        }
        #endregion
    }
}

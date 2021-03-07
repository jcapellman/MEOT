using System;

namespace MEOT.lib.Objects.Base
{
    public class BaseObject
    {
        public int Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }

        public BaseObject()
        {
            Created = DateTimeOffset.Now;

            Modified = DateTimeOffset.Now;
        }
    }
}
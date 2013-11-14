using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Window;
using SFML.Graphics;

namespace TRPGChatRoom.GUI.Map
{
    class CMapGrid
    {
        private Vector2i mapPos_;

        private int terrain_;
        private int mark_;
        private int character_;

        public CMapGrid(Vector2i _mapPos)
        {
        }

        public int Terrain
        {
            get
            {
                return this.terrain_;
            }
            set
            {
                this.terrain_ = value;
            }
        }
    }
}

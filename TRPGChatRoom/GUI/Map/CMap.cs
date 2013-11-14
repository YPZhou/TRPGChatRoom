using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Window;
using SFML.Graphics;

namespace TRPGChatRoom.GUI.Map
{
    class CMap
    {
        private CMapGrid[,] map_;
        private Vector2i crtMapDimension_;
        private Vector2i viewOffset_;
        private Vector2i mapDragStartPos_;
        private Boolean mouseRightButtonPressed_;
        private Boolean mouseLeftButtonPressed_;

        private int terrainSelectPage_;
        private int selectedTerrain_;
        private Vector2i selectedGrid_;

        private List<CCharacter> characterList_;

        private Dictionary<int, Texture> terrainTextureDict_;
        private Sprite[,] terrainSprites_;
        private Sprite[,] characterSprites_;
        private Sprite[,] markSprites_;
        private Sprite[] terrainSelectSprites_;
        private RectangleShape selectedGridRect_;

        // UI state
        // 0 map view mode
        // 1 mouse middle button down, user is dragging the map around
        // 2 mouse right button clicked, terrain selection view
        private int sfmlState_;

        public CMap()
        {
            this.crtMapDimension_ = new Vector2i(20, 12);
            this.viewOffset_ = new Vector2i(0, 0);
            this.mapDragStartPos_ = new Vector2i(0, 0);
            this.mouseRightButtonPressed_ = false;
            this.mouseLeftButtonPressed_ = false;
            this.terrainSelectPage_ = 0;
            this.terrainTextureDict_ = new Dictionary<int, Texture>();
            this.characterList_ = new List<CCharacter>();

            this.sfmlState_ = 0;
        }

        public void Init()
        {
            //this.textureDict_.Add(-1, new Texture("Texture/UI/selected grid.jpg"));
            this.terrainTextureDict_.Add(0, new Texture("Texture/Terrain/burning forest.jpg"));
            this.terrainTextureDict_.Add(1, new Texture("Texture/Terrain/cave.jpg"));
            this.terrainTextureDict_.Add(2, new Texture("Texture/Terrain/chest.jpg"));
            this.terrainTextureDict_.Add(3, new Texture("Texture/Terrain/city wall(side).jpg"));
            this.terrainTextureDict_.Add(4, new Texture("Texture/Terrain/city wall.jpg"));
            this.terrainTextureDict_.Add(5, new Texture("Texture/Terrain/farm.jpg"));
            this.terrainTextureDict_.Add(6, new Texture("Texture/Terrain/farm2.jpg"));
            this.terrainTextureDict_.Add(7, new Texture("Texture/Terrain/forest.jpg"));
            this.terrainTextureDict_.Add(8, new Texture("Texture/Terrain/grass.jpg"));
            this.terrainTextureDict_.Add(9, new Texture("Texture/Terrain/hill.jpg"));
            this.terrainTextureDict_.Add(10, new Texture("Texture/Terrain/lava.jpg"));
            this.terrainTextureDict_.Add(11, new Texture("Texture/Terrain/mountain.jpg"));
            this.terrainTextureDict_.Add(12, new Texture("Texture/Terrain/plain.jpg"));
            this.terrainTextureDict_.Add(13, new Texture("Texture/Terrain/rune.jpg"));
            this.terrainTextureDict_.Add(14, new Texture("Texture/Terrain/sand.jpg"));
            this.terrainTextureDict_.Add(15, new Texture("Texture/Terrain/snow.jpg"));
            this.terrainTextureDict_.Add(16, new Texture("Texture/Terrain/village.jpg"));
            this.terrainTextureDict_.Add(17, new Texture("Texture/Terrain/waste.jpg"));
            this.terrainTextureDict_.Add(18, new Texture("Texture/Terrain/water.jpg"));

            this.map_ = new CMapGrid[this.crtMapDimension_.X, this.crtMapDimension_.Y];
            this.terrainSprites_ = new Sprite[this.crtMapDimension_.X, this.crtMapDimension_.Y];
            this.characterSprites_ = new Sprite[this.crtMapDimension_.X, this.crtMapDimension_.Y];
            this.markSprites_ = new Sprite[this.crtMapDimension_.X, this.crtMapDimension_.Y];
            for (int i = 0; i < this.crtMapDimension_.X; ++i)
            {
                for (int j = 0; j < this.crtMapDimension_.Y; ++j)
                {
                    this.map_[i, j] = new CMapGrid(new Vector2i(i, j));
                    this.terrainSprites_[i, j] = new Sprite();
                    this.terrainSprites_[i, j].Texture = this.terrainTextureDict_[8];
                    this.terrainSprites_[i, j].Scale = new Vector2f(40 / 70.0f, 40 / 70.0f);
                    this.characterSprites_[i, j] = null;
                    this.markSprites_[i, j] = null;
                }
            }

            this.terrainSelectSprites_ = new Sprite[this.terrainTextureDict_.Count];
            for (int i = 0; i < this.terrainTextureDict_.Count; i++)
            {
                this.terrainSelectSprites_[i] = new Sprite();
                this.terrainSelectSprites_[i].Texture = this.terrainTextureDict_[i];
                this.terrainSelectSprites_[i].Scale = new Vector2f(40 / 70.0f, 40 / 70.0f);
            }

            this.selectedGridRect_ = new RectangleShape();
            this.selectedGridRect_.Size = new Vector2f(40, 40);
            this.selectedGridRect_.OutlineThickness = 2;
            this.selectedGridRect_.OutlineColor = Color.White;
            this.selectedGridRect_.FillColor = new Color(0, 0, 0, 0);
        }

        public void Update(RenderWindow _renderWindow)
        {
            switch (this.sfmlState_)
            {
                case 0:
                    if (!this.mouseRightButtonPressed_ && Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        this.sfmlState_ = 2;
                        this.mouseRightButtonPressed_ = true;
                    }
                    else if (Mouse.IsButtonPressed(Mouse.Button.Middle))
                    {
                        this.sfmlState_ = 1;
                        this.mapDragStartPos_ = Mouse.GetPosition(_renderWindow);
                    }
                    else if (!Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        this.mouseRightButtonPressed_ = false;
                    }
                    Vector2i mousePos = Mouse.GetPosition(_renderWindow);
                    int mapPosX = mousePos.X - this.viewOffset_.X;
                    int mapPosY = mousePos.Y - this.viewOffset_.Y;
                    if (mapPosX < 0) mapPosX -= 40;
                    if (mapPosY < 0) mapPosY -= 40;
                    this.selectedGrid_ = new Vector2i(mapPosX / 40, mapPosY / 40);
                    break;
                case 1:
                    this.viewOffset_ += Mouse.GetPosition(_renderWindow) - this.mapDragStartPos_;
                    this.mapDragStartPos_ = Mouse.GetPosition(_renderWindow);
                    if (!Mouse.IsButtonPressed(Mouse.Button.Middle))
                    {
                        this.sfmlState_ = 0;
                    }
                    break;
                case 2:
                    if (!this.mouseRightButtonPressed_ && Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        this.sfmlState_ = 0;
                        this.mouseRightButtonPressed_ = true;
                    }
                    else if (!Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        this.mouseRightButtonPressed_ = false;
                    }

                    if (!this.mouseLeftButtonPressed_ && Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        if (this.selectedTerrain_ != -1)
                        {
                            if (this.selectedGrid_.X >= 0 &&
                                this.selectedGrid_.X < this.crtMapDimension_.X &&
                                this.selectedGrid_.Y >= 0 &&
                                this.selectedGrid_.Y < this.crtMapDimension_.Y)
                            {
                                this.sfmlState_ = 0;
                                this.terrainSprites_[this.selectedGrid_.X, this.selectedGrid_.Y].Texture = this.terrainTextureDict_[this.selectedTerrain_];
                                this.map_[this.selectedGrid_.X, this.selectedGrid_.Y].Terrain = this.selectedTerrain_;
                            }
                            else
                            {
                                int prevMapX = this.selectedGrid_.X < 0 ? 0 - this.selectedGrid_.X : 0;
                                int prevMapY = this.selectedGrid_.Y < 0 ? 0 - this.selectedGrid_.Y : 0;
                                int crtMapX = Math.Max(this.selectedGrid_.X + 1, this.crtMapDimension_.X) - Math.Min(this.selectedGrid_.X, 0);
                                int crtMapY = Math.Max(this.selectedGrid_.Y + 1, this.crtMapDimension_.Y) - Math.Min(this.selectedGrid_.Y, 0);
                                int selectedX = this.selectedGrid_.X <= 0 ? 0 : crtMapX - 1;
                                int selectedY = this.selectedGrid_.Y <= 0 ? 0 : crtMapY - 1;

                                CMapGrid[,] tempMap = new CMapGrid[crtMapX, crtMapY];
                                Sprite[,] tempTerrainSprites = new Sprite[crtMapX, crtMapY];
                                Sprite[,] tempCharacterSprites = new Sprite[crtMapX, crtMapY];
                                Sprite[,] tempMarkSprites = new Sprite[crtMapX, crtMapY];
                                for (int i = 0; i < crtMapX; ++i)
                                {
                                    for (int j = 0; j < crtMapY; ++j)
                                    {
                                        tempMap[i, j] = new CMapGrid(new Vector2i(i, j));
                                        tempTerrainSprites[i, j] = new Sprite();
                                        if (i >= prevMapX &&
                                            i < prevMapX + this.crtMapDimension_.X &&
                                            j >= prevMapY &&
                                            j < prevMapY + this.crtMapDimension_.Y)
                                        {
                                            tempTerrainSprites[i, j].Texture = this.terrainSprites_[i - prevMapX, j - prevMapY].Texture;
                                        }
                                        else if (i == selectedX && j == selectedY)
                                        {
                                            tempTerrainSprites[i, j].Texture = this.terrainTextureDict_[this.selectedTerrain_];
                                        }
                                        else
                                        {
                                            tempTerrainSprites[i, j].Texture = this.terrainTextureDict_[8];
                                        }
                                        tempTerrainSprites[i, j].Scale = new Vector2f(40 / 70.0f, 40 / 70.0f);
                                        tempCharacterSprites[i, j] = null;
                                        tempMarkSprites[i, j] = null;
                                    }
                                }

                                this.crtMapDimension_.X = crtMapX;
                                this.crtMapDimension_.Y = crtMapY;
                                this.map_ = tempMap;
                                this.terrainSprites_ = tempTerrainSprites;
                                this.characterSprites_ = tempCharacterSprites;
                                this.markSprites_ = tempMarkSprites;

                                this.sfmlState_ = 0;
                            }
                        }
                    }
                    else if (!Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        this.mouseLeftButtonPressed_ = false;
                    }

                    this.selectedTerrain_ = -1;
                    mousePos = Mouse.GetPosition(_renderWindow);
                    for (int i = 0; i < this.terrainTextureDict_.Count; i++)
                    {
                        if (i >= this.terrainSelectPage_ * 30 && i < (this.terrainSelectPage_ + 1) * 30)
                        {
                            if (mousePos.X >= this.terrainSelectSprites_[i].Position.X &&
                                mousePos.X <= this.terrainSelectSprites_[i].Position.X + 40 &&
                                mousePos.Y >= this.terrainSelectSprites_[i].Position.Y &&
                                mousePos.Y <= this.terrainSelectSprites_[i].Position.Y + 40)
                            {
                                this.selectedTerrain_ = i;
                            }
                        }
                    }
                    break;
            }
        }

        public void Render(RenderWindow _renderWindow)
        {
            switch (this.sfmlState_)
            {
                // map view mode
                // or
                // mouse middle button down
                case 0:
                case 1:
                    for (int i = 0; i < this.crtMapDimension_.X; i++)
                    {
                        for (int j = 0; j < this.crtMapDimension_.Y; j++)
                        {
                            if (this.terrainSprites_[i, j] != null)
                            {
                                this.terrainSprites_[i, j].Position = new Vector2f(i * 40 + this.viewOffset_.X, j * 40 + this.viewOffset_.Y);
                                this.terrainSprites_[i, j].Color = Color.White;
                                _renderWindow.Draw(this.terrainSprites_[i, j]);
                            }
                            if (this.characterSprites_[i, j] != null)
                            {
                                _renderWindow.Draw(this.characterSprites_[i, j]);
                            }
                            if (this.markSprites_[i, j] != null)
                            {
                                _renderWindow.Draw(this.markSprites_[i, j]);
                            }
                        }
                    }
                    this.selectedGridRect_.Position = new Vector2f(this.selectedGrid_.X * 40 + this.viewOffset_.X, this.selectedGrid_.Y * 40 + this.viewOffset_.Y);
                    _renderWindow.Draw(this.selectedGridRect_);
                    break;
                case 2:
                    for (int i = 0; i < this.crtMapDimension_.X; i++)
                    {
                        for (int j = 0; j < this.crtMapDimension_.Y; j++)
                        {
                            if (this.terrainSprites_[i, j] != null)
                            {
                                this.terrainSprites_[i, j].Position = new Vector2f(i * 40 + this.viewOffset_.X, j * 40 + this.viewOffset_.Y);
                                this.terrainSprites_[i, j].Color = new Color(255, 255, 255, 100);
                                _renderWindow.Draw(this.terrainSprites_[i, j]);
                            }
                            if (this.characterSprites_[i, j] != null)
                            {
                                _renderWindow.Draw(this.characterSprites_[i, j]);
                            }
                            if (this.markSprites_[i, j] != null)
                            {
                                _renderWindow.Draw(this.markSprites_[i, j]);
                            }
                        }
                    }
                    for (int i = 0; i < this.terrainTextureDict_.Count; i++)
                    {
                        if (i >= this.terrainSelectPage_ * 30 && i < (this.terrainSelectPage_ + 1) * 30)
                        {
                            int x = (i - this.terrainSelectPage_ * 30) % 10;
                            int y = (i - this.terrainSelectPage_ * 30) / 10;
                            this.terrainSelectSprites_[i].Position = new Vector2f(110 + x * 60, 135 + y * 110);
                            _renderWindow.Draw(this.terrainSelectSprites_[i]);

                            if (selectedTerrain_ == i)
                            {
                                this.selectedGridRect_.Position = new Vector2f(110 + x * 60, 135 + y * 110);
                                _renderWindow.Draw(this.selectedGridRect_);
                            }
                        }
                    }
                    break;
            }
        }
    }
}

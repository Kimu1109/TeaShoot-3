using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;
using static DxLibDLL.DX;

namespace TeaShoot_3
{
    [Serializable]
    [TypeConverter(typeof(DefinitionOrderTypeConverter))]
    public class obj
    {
        [Category("タイプ")]
        public int num { get; set; }
        [Category("タイプ")]
        public ObjType type { get; set; }
        [Category("タイプ")]
        public MoveType move { get; set; }
        [Category("タイプ")]
        public AttackType attack { get; set; }
        [Category("タイプ")]
        public RemoveType remove { get; set; }
        [Category("タイプ")]
        public InitType init { get; set; }
        [Category("タイプ")]
        public int hp { get; set; }
        [Category("ダメージ")]
        public int damage { get; set; }
        public int maxHP;
        [Category("タイプ")]
        public int shotNum { get; set; }
        [Category("タイプ")]
        public int score { get; set; }
        [Category("タイプ")]
        public bool IsBoss { get; set; }

        [Category("位置")]
        public float x { get; set; }
        [Category("位置")]
        public float y { get; set; }
        [Category("位置")]
        public float width { get; set; }
        [Category("位置")]
        public float height { get; set; }




        [Category("移動")]
        public float speedX { get; set; }
        [Category("移動")]
        public float speedY { get; set; }
        [Category("移動")]
        public double angle { get; set; }




        [Category("表示")]
        public string text { get; set; }
        [Category("表示")]
        public Color TextColor
        {
            get
            {
                int r; int g; int b;
                GetColor2(textColor, out r, out g, out b);
                return Color.FromArgb(r, g, b);
            }
            set
            {
                textColor = GetColor(value.R, value.G, value.B);
            }
        }
        [Category("バウンド")]
        public int RemoveBoundCountMax { get; set; }
        [Category("バウンド")]
        public int SuperBoundNum { get; set; }
        public int SuperBoundCount;
        public bool IsBound;

        public int RemoveBoundCount;

        public uint textColor;
        public int shotInterval;

        public bool isInit;

        [Category("プレイヤー")]
        public int residue { get; set; }
        public int residueFlash;

        public boss2 boss2;
        public boss1 boss1;

        //Shakeはボールがバウンドする
        [Category("プレイヤー")]
        public bool ShakeEnabled { get; set; }

        //共通変数　及び　環境変数
        public static int camX;
        public static obj player;
        public static Random rnd = new Random();

        public static List<obj> objList;
        public static List<obj> removeList;
        public static List<obj> resistList;

        public static bool isDevelop;

        public static int autoX;
        public static bool IsScroll;
        public static int ScrollNum;
        public static int ScrollX;

        public static int BuildNum;
        public static string LastBuild;

        //FPS管理変数
        public static int fps;
        public static long idealSleep;
        public static long oldTime;
        public static long newTime;
        public static long error;

        public static long startTime;
        public static int FpsCount;
        public static int FPS;

        public static string DevFileName;

        public static long debugTime;
        public static long debugStartTime;

        public static bool IsF4;
        public static int MouseInput;
        public static int posX;
        public static int posY;

        public const int Boss1 = 13;

        public void Draw()
        {
            switch (num)
            {
                case 0:
                    if (residueFlash == 0 || residueFlash % 2 == 0)
                    {
                        DrawString((int)x - camX, (int)y, text, textColor);
                        DrawBox(0, 0, player.hp, 20, textColor, 1);
                    } else {
                        DrawString((int)x - camX, (int)y, text, GetColor(0, 0, 0));
                        DrawBox(0, 0, player.hp, 20, GetColor(0, 0, 0), 1);
                    }
                    residueFlash = Math.Max(0, residueFlash - 1);
                    if (residueFlash != 0) hp = 255 - residueFlash;
                    DrawBox(0, 0, 255, 20, GetColor(255, 255, 255), 0);
                    break;
                case 1:
                    if (player.residueFlash == 0 || player.residueFlash % 2 == 0)
                        DrawString((int)x - camX, (int)y, text, textColor);
                    else
                        DrawString((int)x - camX, (int)y, text, GetColor(0, 0, 0));
                    break;
                default:
                    DrawString((int)x - camX, (int)y, text, textColor);
                    if (IsBoss)
                    {
                        DrawBox(385, 0, 640, 20, GetColor(255,255,255), 0);
                        DrawBox(385, 0, 385 + (int)((float)hp / (float)Math.Max(1, maxHP) * 255), 20, textColor, 1);
                    }
                    break;
            }
        }
        public void Process(int i)
        {
            if (num == 0 && hp <= 0)
            {
                residue--;
                residueFlash = maxHP;
                if (residue < 0)
                {
                    MsgBox("GAME OVER");
                }
            }

            if (!isInit)
            {
                isInit = true;
                IsBound = true;
                maxHP = hp;
                switch (init)
                {
                    case InitType.Nothing:
                        break;
                    case InitType.X640_YAuto:
                        x = 640;
                        break;
                    case InitType.X0_YAuto:
                        x = 0 - width;
                        break;
                    case InitType.Y0_XAuto:
                        autoX += 50;
                        if (autoX > 640) { autoX = 0; }
                        x = autoX;
                        y = 0;
                        break;
                    case InitType.Y480_XAuto:
                        autoX += 50;
                        if (autoX > 640) { autoX = 0; }
                        x = autoX;
                        y = 480 - height;
                        break;
                }
            }

            double POAngle;
            switch (move)
            {
                case MoveType.Speed:
                    x += speedX;
                    y += speedY;
                    break;
                case MoveType.Angle:
                    x += (float)Math.Cos(angle);
                    y += (float)Math.Sin(angle);
                    break;
                case MoveType.ShakeAndNear:
                    POAngle = Math.Atan2(y - player.y, x - player.x);
                    x -= (float)(Math.Cos(POAngle) + (rnd.NextDouble() - 0.5) * 3);
                    y -= (float)(Math.Sin(POAngle) + (rnd.NextDouble() - 0.5) * 3);
                    break;
                case MoveType.LittleNear:
                    POAngle = Math.Atan2(y - player.y, x - player.x);
                    x -= (float)(Math.Cos(POAngle) * 2);
                    y -= (float)(Math.Sin(POAngle) * 2);
                    if (Math.Sqrt(Math.Pow(x - player.x, 2) + Math.Pow(y - player.y, 2)) < 70)
                    {
                        move = MoveType.Speed;
                        speedX = -(float)(Math.Cos(POAngle) * 2);
                        speedY = -(float)(Math.Sin(POAngle) * 2);
                    }
                    break;
                case MoveType.Bound:
                    x += speedX;
                    y += speedY;
                    if (y < 0 || y > 480 - height)
                    {
                        speedY *= -1;
                        RemoveBoundCount++;
                        if (RemoveBoundCountMax != -1 && RemoveBoundCount >= RemoveBoundCountMax)
                        {
                            move = MoveType.Speed;
                            speedY *= -1;
                        }
                    }
                    break;
                case MoveType.BoundX0:

                    x += speedX;
                    y += speedY;
                    if (x < 0) speedX *= -1;
                    break;
                case MoveType.LittleNearAndGoAwayFromBall:
                    POAngle = Math.Atan2(y - player.y, x - player.x);
                    x -= (float)(Math.Cos(POAngle) * 2);
                    y -= (float)(Math.Sin(POAngle) * 2);
                    if (Math.Sqrt(Math.Pow(x - player.x, 2) + Math.Pow(y - player.y, 2)) < 70)
                    {
                        move = MoveType.Speed;
                        speedX = -(float)(Math.Cos(POAngle) * 2);
                        speedY = -(float)(Math.Sin(POAngle) * 2);
                    }
                    foreach (var o in objList)
                    {
                        if (o.type == ObjType.Ball)
                        {
                            if (DistanceP(point, o.point) <= 100)
                            {
                                TwoPointToSpeed(this, this.point, o.point);
                                x -= speedX;
                                y -= speedY;
                            }
                        }
                    }
                    break;
                case MoveType.BoundSuper:

                    speedY += 0.02f;

                    if (IsBound)
                    {
                        switch (IsAllDrawingRange(true))
                        {
                            case 0:
                                break;
                            case 1:
                                speedY *= -0.8f;
                                SuperBoundCount++;
                                if (SuperBoundCount >= SuperBoundNum)
                                {
                                    speedY *= 2;
                                    SuperBoundCount = 0;
                                }
                                RemoveBoundCount++;
                                if ((RemoveBoundCountMax != -1) && (RemoveBoundCountMax <= RemoveBoundCount)) move = MoveType.Speed;
                                break;
                            case 2:
                                speedX *= -0.8f;
                                SuperBoundCount++;
                                if (SuperBoundCount >= SuperBoundNum)
                                {
                                    speedX *= 2;
                                    SuperBoundCount = 0;
                                }
                                RemoveBoundCount++;
                                if ((RemoveBoundCountMax != -1) && (RemoveBoundCountMax <= RemoveBoundCount)) move = MoveType.Speed;
                                break;
                        }
                    }

                    x += speedX;
                    y += speedY;

                    break;
                case MoveType.Gravity:
                    x += speedX;
                    y += speedY;
                    speedY += 0.03f;
                    break;
                case MoveType.PlayerBall:
                    x += speedX;
                    y += speedY;
                    if (player.ShakeEnabled)
                    {
                        if (y <= 0 || y >= 480 - height)
                        {
                            speedY *= -1;
                            move = MoveType.Speed;
                        }
                        if (x <= 0 || x >= 640 - width)
                        {
                            speedX *= -1;
                            move = MoveType.Speed;
                        }
                    }


                    break;
                case MoveType.Shake:

                    x += speedX;
                    y += speedY;

                    speedX = Math.Min(0, speedX + 0.01f);
                    speedY += 0.02f;

                    switch (IsAllDrawingRange(true))
                    {
                        case 0:
                            break;
                        case 1:
                            speedY *= -0.6f;
                            break;
                        case 2:
                            speedY *= -0.6f;
                            break;
                    }

                    break;
            }
            switch (attack)
            {
                case AttackType.Normal:
                    shotInterval++;
                    if (shotInterval > 80)
                    {
                        shotInterval = 0;
                        var shotObj = obj.Clone(ResistIndexOf(shotNum));
                        shotObj.x = x;
                        shotObj.y = y;
                        shotObj.isInit = true;
                        objList.Add(shotObj);
                    }
                    break;
                case AttackType.Boss1:
                    bosses.ProcessBoss1(i);
                    break;
                case AttackType.Boss1_Fishing_Rod:
                    if (y <= 0)
                    {
                        y++;
                    }
                    else if (shotInterval != -1)
                    {
                        shotInterval++;
                        if (shotInterval == 150)
                        {
                            var o14 = Clone(ResistIndexOf(14));
                            o14.x = x;
                            o14.y = 480;
                            o14.FitText(ReadAscii("boss1-fish"));
                            o14.speedY = -5;
                            o14.hp = 1000;
                            o14.move = MoveType.Gravity;
                            o14.remove = RemoveType.Big;
                            o14.type = ObjType.Enemy;
                            objList.Add(o14);
                        }
                        foreach (var o in objList)
                        {
                            if (o.num == 14 && o.move == MoveType.Gravity && o.x == x && o.y <= height)
                            {
                                o.move = MoveType.Speed;
                                o.speedY = -1;
                                shotInterval = -1;
                                break;
                            }
                        }
                    }
                    if (shotInterval == -1) y -= 2;
                    break;
            }

            if (!isDevelop)
            {
                if (!(x >= 0 - width - 100 * (int)remove && x <= 640 + 100 * (int)remove && y >= 0 - height - 100 * (int)remove && y <= 480 + height + 100 * (int)remove))
                {
                    removeList.Add(this);
                }
                if (hp <= 0)
                {
                    removeList.Add(this);
                }
            }

            int touchIndex;
            switch (type)
            {
                case ObjType.Ball:
                    touchIndex = Touch_obj(i, new ObjType[] { ObjType.Player, ObjType.Ball, ObjType.Shake });
                    if (touchIndex != -1)
                    {
                        if (!isDevelop) removeList.Add(this);
                        objList[touchIndex].hp--;
                        if (objList[touchIndex].hp <= 0)
                        {
                            player.score += objList[touchIndex].score;
                        }
                    }
                    break;
                case ObjType.Player:
                    touchIndex = Touch_obj(i, new ObjType[] { ObjType.Player, ObjType.Ball });
                    if (touchIndex != -1)
                    {
                        switch (objList[touchIndex].type)
                        {
                            case ObjType.Enemy:
                                hp--;
                                break;
                            case ObjType.EnemyBall:
                                hp -= objList[touchIndex].damage;
                                removeList.Add(objList[touchIndex]);
                                break;
                            case ObjType.Shake:
                                MsgBox("シャケのパワーで、\n弾が反射するようになった!");
                                ShakeEnabled = true;
                                removeList.Add(objList[touchIndex]);
                                break;
                        }
                    }
                    break;
            }

        }
        public bool RemoveEvent()
        {
            switch (num)
            {
                case 0:
                    return false;
                case Boss1:
                    boss1.attack = boss1.attackType.MoveLast;
                    return boss1.IsRemove;

                default:
                    return true;
            }
        }
        public void FitText(string text)
        {
            this.text = text;
            this.height = GetFontSize() * text.Replace("\r\n", "\n").Split(new[] { '\n', '\r' }).Count();
            this.width = GetDrawStringWidth(text, -1);
        }
        public static obj Clone(obj copy)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, copy);
                stream.Position = 0;

                return (obj)formatter.Deserialize(stream);
            }
        }
        public static void WriteObj(obj obj, string path)
        {
            if (obj == null) return;
            obj.boss2 = new boss2();
            obj.boss1 = new boss1();
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
        }

        public obj(int num, ObjType type, bool IsFitSize = true, float width = 0, float height = 0, string text = "")
        {
            if (IsFitSize)
            {
                this.height = GetFontSize() * text.Replace("\r\n", "\n").Split(new[] { '\n', '\r' }).Count();
                this.width = GetDrawStringWidth(text, -1);
            }
            else
            {
                this.width = width;
                this.height = height;
            }
            this.text = text;
            this.type = type;
            this.num = num;
            this.textColor = GetColor(255, 255, 255);
            this.remove = RemoveType.Normal;
        }
        /// <summary>
        /// いわゆる当たり判定
        /// </summary>
        /// <param name="i">当たっているかを確認するオブジェクト</param>
        /// <returns></returns>
        public static int Touch_obj(int i, ObjType[] target)
        {
            var TObj = objList[i];
            if (!(TObj.x >= -TObj.width + camX && TObj.x <= 640 + camX)) return -1;
            for (int z = 0; z < objList.Count; z++)
            {
                if (z != i)
                {
                    var CObj = objList[z];
                    if (CObj.x >= -TObj.width + camX && CObj.x <= 640 + camX)
                    {
                        if (TObj.x < CObj.x + CObj.width &&
                            TObj.x + TObj.width > CObj.x &&
                            TObj.y < CObj.y + CObj.height &&
                            TObj.y + TObj.height > CObj.y &&
                            (target[0] == ObjType.All || Array.IndexOf(target, CObj.type) == -1))
                        {
                            return z;
                        }
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 登録オブジェクトを再取得
        /// </summary>
        public static void ReloadResist()
        {
            resistList.Clear();
            string[] names = Directory.GetFiles(obj.AppPath() + @"\resist", "*");
            foreach (string name in names)
            {
                try
                {
                    using (var fs = new FileStream(name, FileMode.Open, FileAccess.Read))
                    {
                        var formatter = new BinaryFormatter();
                        resistList.Add((obj)formatter.Deserialize(fs));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// <summary>
        /// 登録オブジェクトのIndexOf
        /// </summary>
        /// <param name="num">探すobjのnum</param>
        /// <returns></returns>
        public static obj ResistIndexOf(int num)
        {
            foreach (var item in resistList)
            {
                if (item.num == num)
                {
                    return item;
                }
            }
            return resistList[0];
        }
        /// <summary>
        /// asciiフォルダの.txtファイルから文字列を読み込み、返します。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadAscii(string fileName)
        {
            using (var sr = new StreamReader(@".\ascii\" + fileName + ".txt"))
            {
                return sr.ReadToEnd();
            }
        }
        public static uint ColorReverse(uint color)
        {
            int r, g, b;
            GetColor2(color, out r, out g, out b);
            r = 255 - r;
            g = 255 - g;
            b = 255 - b;
            return GetColor(r, g, b);
        }

        public static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
        public static double DistanceP(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="type">0はそのまま
        /// 1は足し算
        /// 2は引き算
        /// </param>
        public static void TwoPointToSpeed(obj o, Point p1, Point p2, int type = 0)
        {
            double angle;
            switch (type)
            {
                case 0:
                    angle = Math.Atan2(p2.y - p1.y, p2.x - p1.x);
                    o.speedX = (float)Math.Cos(angle);
                    o.speedY = (float)Math.Sin(angle);
                    break;
                case 1:
                    angle = Math.Atan2(p2.y - p1.y, p2.x - p1.x);
                    o.speedX += (float)Math.Cos(angle);
                    o.speedY += (float)Math.Sin(angle);
                    break;
                case 2:
                    angle = Math.Atan2(p2.y - p1.y, p2.x - p1.x);
                    o.speedX -= (float)Math.Cos(angle);
                    o.speedY -= (float)Math.Sin(angle);
                    break;
            }
        }
        /// <summary>
        /// 0 = 入ってる 
        /// 1=Y座標が原因で入っていない 
        /// 2=X座標が原因で入っていない
        /// </summary>
        /// <returns></returns>
        public int IsAllDrawingRange(bool IsFit = false)
        {
            if (x >= 0 && x <= 640 - width && y >= 0 && y <= 480 - height)
                return 0;
            else
            {
                if (x >= 0 && x <= 640 - width)
                {
                    if (IsFit)
                    {
                        if (!(x >= 0)) x = 0;
                        if (!(x <= 640 - width)) x = 640 - width;
                        if (!(y >= 0)) y = 0;
                        if (!(y <= 480 - height)) y = 480 - height;
                    }
                    return 1;
                }
                else
                {
                    if (IsFit)
                    {
                        if (!(x >= 0)) x = 0;
                        if (!(x <= 640 - width)) x = 640 - width;
                        if (!(y >= 0)) y = 0;
                        if (!(y <= 480 - height)) y = 480 - height;
                    }
                    return 2;
                }
            }
        }
        public static bool IsClickRect(Rect rect)
        {
            if (MouseInput != 0 && posX >= rect.point1.x && posX <= rect.point2.x && posY >= rect.point1.y && posY <= rect.point2.y)
                return true;
            else
                return false;
        }
        public static MsgBoxResult MsgBox(string Text, string Title = "TeaShoot3", MsgBoxButton button = MsgBoxButton.OK)
        {
            const int interval = 25;

            var TextArr = Text.Replace("\r\n", "\n").Split(new[] { '\n', '\r' });
            string newText = "";
            foreach(var item in TextArr)
            {
                if(GetDrawStringWidth(item,-1) >= 340 - interval * 4)
                {
                    for(int i = item.Length; i > 0; i--)
                    {
                        if(GetDrawStringWidth(Strings.Mid(item, 1, i), -1) <= 340 - interval * 4)
                        {
                            newText += item.Insert(i, "\n");
                            break;
                        }
                    }
                }
                else
                {
                    newText += item + "\n";
                }
            }
            Text = newText;

            Rect OK = default;
            Rect No = default;
            Rect Cancel = default;

            int useX = interval + 150;

            if (button.HasFlag(MsgBoxButton.Cancel))
            {
                Cancel = new Rect(new Point(useX, 290), new Point(useX + 80, 320));
                useX += 80 + interval;
            }
            if (button.HasFlag(MsgBoxButton.No)){
                No = new Rect(new Point(useX, 290), new Point(useX + 80, 320));
                useX += 80 + interval;
            }
            if (button.HasFlag(MsgBoxButton.OK))
            {
                OK = new Rect(new Point(useX, 290), new Point(useX + 80, 320));
                useX += 80 + interval;
            }

            while (ProcessMessage() != -1)
            {
                FPS_Controller_Before();

                if(OK != default)
                {
                    DrawBox((int)OK.point1.x, (int)OK.point1.y, (int)OK.point2.x, (int)OK.point2.y, GetColor(255, 255, 255), 0);
                    DrawString((int)OK.point1.x + ((int)OK.point2.x - (int)OK.point1.x - GetDrawStringWidth("OK", -1)) / 2, (int)OK.point1.y + ((int)OK.point2.y - (int)OK.point1.y - GetFontSize()) / 2, "OK", GetColor(255, 255, 255));
                    if (IsClickRect(OK)) { return MsgBoxResult.OK; }
                }
                if(No != default)
                {
                    DrawBox((int)No.point1.x, (int)No.point1.y, (int)No.point2.x, (int)No.point2.y, GetColor(255, 255, 255), 0);
                    DrawString((int)No.point1.x + ((int)No.point2.x - (int)No.point1.x - GetDrawStringWidth("No", -1)) / 2, (int)No.point1.y + ((int)No.point2.y - (int)No.point1.y - GetFontSize()) / 2, "No", GetColor(255, 255, 255));
                    if (IsClickRect(No)) { return MsgBoxResult.No; }
                }
                if (Cancel != default)
                {
                    DrawBox((int)Cancel.point1.x, (int)Cancel.point1.y, (int)Cancel.point2.x, (int)Cancel.point2.y, GetColor(255, 255, 255), 0);
                    DrawString((int)Cancel.point1.x + ((int)Cancel.point2.x - (int)Cancel.point1.x - GetDrawStringWidth("Cancel", -1)) / 2, (int)Cancel.point1.y + ((int)Cancel.point2.y - (int)Cancel.point1.y - GetFontSize()) / 2, "Cancel", GetColor(255, 255, 255));
                    if (IsClickRect(Cancel)) { return MsgBoxResult.Cancel; }
                }

                DrawBox(150, 150, 490, 330, GetColor(255, 255, 255), 0);
                DrawBox(150, 150, 490, 150 + interval, GetColor(255, 255, 255), 1);
                DrawString(155, 150 + (interval - GetFontSize()) / 2, Title, GetColor(0, 0, 0));
                DrawString(150 + interval * (int)2, 150 + interval * (int)2, Text, GetColor(255, 255, 255));

                FPS_Controller_After();
            }
            return MsgBoxResult.Cancel;
        }
        public enum MsgBoxResult
        {
            OK = 0,
            No = 1,
            Cancel = 2
        }
        public enum MsgBoxButton
        {
            OK = 0b0001,
            No = 0b0010,
            Cancel = 0b0100
        }
        public enum MoveType
        {
            Nothing = -1,
            Speed = 0,
            Angle = 1,
            ShakeAndNear = 2,
            LittleNear = 3,
            Bound = 4,
            BoundX0 = 5,
            LittleNearAndGoAwayFromBall = 6,
            BoundSuper = 7,
            Gravity = 8,
            PlayerBall = 9,
            Shake = 10
        }
        //命名法則: 基本逆で書くこと,Fire=間隔が早い,Little=間隔が遅い or 効果が弱い
        public enum AttackType
        {
            Nothing = 0,
            Normal = 1,
            NormalFire = 2,
            Boss1 = 3,
            Boss1_Fishing_Rod = 300,
            Boss1_Fishing_Fish = 301,
        }
        public enum RemoveType
        {
            Small = 0,
            Normal = 1,
            NormalPlus = 3,
            Big = 5,
            VeryBig = 8,
            MostBig = 1000,
        }
        public enum ObjType
        {
            All = -1,
            Player = 0,
            Ball = 1,
            Enemy = 10,
            EnemyBall = 11,
            Shake = 12
        }
        public enum InitType
        {
            Nothing = -1,
            X640_YAuto = 0,
            X0_YAuto = 1,
            Y480_XAuto = 3,
            Y0_XAuto = 2
        }
        /// <summary>
        /// FPSの調整
        /// </summary>
        public static void FPS_Controller_After()
        {
            ScreenFlip();

            FpsCount++;
            if (startTime + 1000 < (long)(DateAndTime.Timer * 1000))
            {
                startTime = (long)(DateAndTime.Timer * 1000);
                FPS = FpsCount;
                FpsCount = 0;
            }

            newTime = (long)(DateAndTime.Timer * 1000);
            long sleepTime = idealSleep - (newTime - oldTime) - error; // 休止できる時間  
            oldTime = newTime;
            if (sleepTime < 1) { sleepTime = 1; }
            if (sleepTime > 10) { sleepTime = 10; }
            if (!IsF4) WaitTimer((int)(sleepTime)); // 休止  
            newTime = (long)(DateAndTime.Timer * 1000);
            error = newTime - oldTime - sleepTime; // 休止時間の誤差  
        }
        /// <summary>
        /// FPSの調整前の設定
        /// </summary>
        public static void FPS_Controller_Before()
        {
            if (CheckHitKey(KEY_INPUT_F4) == TRUE) IsF4 = true; else IsF4 = false;

            MouseInput = GetMouseInput();
            GetMousePoint(out posX, out posY);

            oldTime = newTime;
            debugTime = newTime;

            ClearDrawScreen();
            if ((isDevelop && CheckHitKey(KEY_INPUT_Q) == TRUE) || !isDevelop) DrawString(50, 40, FPS.ToString() + "FPS\nObjNum:" + objList.Count.ToString() + "\nBuildNum:" + BuildNum.ToString() + "\nLastBuild:" + LastBuild.ToString() + "\nCamX:" + camX.ToString() + "\nDevFileName:" + DevFileName + "\nDebugTime:" + ((double)(debugTime - debugStartTime) / 1000).ToString() + "s\n予想時間:" + SecondToTime((int)(player.x * 0.02)) + "\nScrollX:" + ScrollX.ToString(), GetColor(255, 255, 255));

            DrawString(0, 20, "Score:" + player.score.ToString(), GetColor(255, 255, 255));
        }

        public static string AppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        public static string SecondToTime(int s)
        {
            string r = "";
            int hour = s / 60 / 60;
            s -= hour * 60 * 60;
            int minute = s / 60;
            s -= minute * 60;
            int second = s;

            if (hour != 0) { r += hour.ToString() + "時"; }
            if (minute != 0) { r += minute.ToString() + "分"; }
            if (second != 0) { r += second.ToString() + "秒"; }
            return r;
        }
        public override string ToString()
        {
            return text + " | " + num.ToString();
        }

        [Browsable(false)]
        public int Cx
        {
            get
            {
                return (int)x - camX;
            }
        }
        [Browsable(false)]
        public Point point
        {
            get
            {
                return new Point(x, y);
            }
        }
    }
    /// <summary>
    /// プロパティグリッドのプロパティの並び順をソート
    /// </summary>
    class DefinitionOrderTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            // TypeDescriptorを使用してプロパティ一覧を取得する
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);

            // プロパティ一覧をリフレクションから取得
            Type type = value.GetType();
            List<string> list = new List<string>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                list.Add(propertyInfo.Name);
            }

            // リフレクションから取得した順でソート
            return pdc.Sort(list.ToArray());
        }

        /// <summary>
        /// GetPropertiesをサポートしていることを表明する。
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    public class Point
    {
        public double x;
        public double y;
        public Point(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Rect
    {
        public Point point1;
        public Point point2;
        public Rect(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static DxLibDLL.DX;
using static TeaShoot_3.obj;

namespace TeaShoot_3
{
    public static class Program
    {

        //FPS管理変数
        static int fps;
        static long idealSleep;
        static long oldTime;
        static long newTime;
        static long error;

        static long startTime;
        static int FpsCount;
        static int FPS;

        //メイン変数
        public static List<obj> selectList;

        static int shotInterval;

        static obj ball;

        static string mapFile;

        static List<obj[]> mapList;

        //開発変数
        static int posX;
        static int posY;

        static int PutX;
        static int PutY;

        static bool IsShortcutPushed;

        static double camXPlus;
        static bool IsClickArrow;

        static bool IsHoldObj;
        static obj HoldObj;

        static PropertyScreen ps;

        static int touchIndexDev;
        static bool isW;
        static bool isA;
        static bool isS;
        static bool isD;

        static int BuildNum;
        static string LastBuild;

        public static string DevFileName;

        static long debugTime;
        static long debugStartTime;

        [STAThread]
        public static void Main()
        {
            //ビルドの取得及び出力
            using(var sr = new StreamReader(AppPath() + @"\output.log"))
            {
                BuildNum = Convert.ToInt32(sr.ReadLine()) + 1;
            }
            using (var sw = new StreamWriter(AppPath() + @"\output.log"))
            {
                sw.WriteLine(BuildNum.ToString());
                sw.WriteLine(DateTime.Now.ToString());
            }
            LastBuild = DateTime.Now.ToString();

            //オブジェクトの初期化
            objList = new List<obj>();
            removeList = new List<obj>();
            resistList = new List<obj>();
            selectList = new List<obj>();
            mapList = new List<obj[]>();
            ps = new PropertyScreen();
            fps = 100;
            idealSleep = 1000 / fps;
            DevFileName = "map1.dat";

            //DxLibの初期設定
            ChangeWindowMode(1);
            SetWaitVSyncFlag(FALSE);
            SetDrawScreen(DX_SCREEN_BACK);
            SetAlwaysRunFlag(TRUE);
            SetWindowSizeExtendRate(1.5);

            if (DxLib_Init() == -1)
            {
                Environment.Exit(0);
            }

            ChangeFont("ＭＳ Ｐゴシック");

            //登録オブジェクトの読み込み及び設定。
            ReloadResist();

            foreach(var o in resistList)
            {
                o.FitText(o.text);
                obj.WriteObj(o, obj.AppPath() + @"\resist\" + o.num.ToString() + ".dat");
            }

            objList.Add(ResistIndexOf(0));
            player = objList[0];
            ball = ResistIndexOf(1);
            player.y = 240 - player.height / 2; 

            newTime = (long)(DateAndTime.Timer * 1000);

            //メインループ開始
            while (ProcessMessage() != -1)
            {
                FPS_Controller_Before();

                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].Draw();
                    if (!isDevelop) { 
                        objList[i].Process(i);
                    }
                }
                //自動消去
                foreach (var o in removeList)
                {
                    if(o != null && o.num != 0) objList.Remove(o);
                }
                removeList.Clear();
                if (!isDevelop) {
                    //スクロール
                        ScrollNum++;
                        if(ScrollNum > 50)
                        {
                            ScrollNum = 0;
                            ScrollX++;
                            if(ScrollX < mapList.Count)
                            {
                                foreach (var abc in mapList[ScrollX])
                                {
                                    if (abc != null)
                                        objList.Add(obj.Clone(abc));
                                }
                            }
                        }
                    //上
                    if (CheckHitKey(KEY_INPUT_W) == TRUE)
                    {
                        player.y -= 2;
                    }
                    //下
                    if (CheckHitKey(KEY_INPUT_S) == TRUE)
                    {
                        player.y += 2;
                    }
                    //左
                    if (CheckHitKey(KEY_INPUT_A) == TRUE)
                    {
                        player.x -= 2;
                    }
                    //右
                    if (CheckHitKey(KEY_INPUT_D) == TRUE)
                    {
                        player.x += 2;
                    }

                    //プレイヤーの位置を無理やり調整
                    if (player.x < 0) player.x = 0;
                    if (player.x > 640 - player.width) player.x = 640 - player.width;

                    if (player.y < 0) player.y = 0;
                    if (player.y > 480 - player.height) player.y = 480 - player.height;

                    //ショット
                    if (CheckHitKey(KEY_INPUT_SPACE) == TRUE && shotInterval == 0)
                    {
                        ball.x = player.x + player.width;
                        ball.y = player.y;
                        objList.Add(obj.Clone(ball));
                        shotInterval = 5;
                    }
                    shotInterval = Math.Max(shotInterval - 1, 0);
                }

                Develop_Controller();
                FPS_Controller_After();
            }

            WaitTimer(2000);
            DxLib_End();

        }
        /// <summary>
        /// FPSの調整
        /// </summary>
        private static void FPS_Controller_After()
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
            if(sleepTime < 1) { sleepTime = 1; }
            WaitTimer((int)(sleepTime)); // 休止  
            newTime = (long)(DateAndTime.Timer * 1000);
            error = newTime - oldTime - sleepTime; // 休止時間の誤差  
        }
        /// <summary>
        /// FPSの調整前の設定
        /// </summary>
        private static void FPS_Controller_Before()
        {
            oldTime = newTime;
            debugTime = newTime;

            ClearDrawScreen();
            if((isDevelop && CheckHitKey(KEY_INPUT_Q) == TRUE) || !isDevelop) DrawString(50, 20, FPS.ToString() + "FPS\nObjNum:" + objList.Count.ToString() + "\nBuildNum:" + BuildNum.ToString() + "\nLastBuild:" + LastBuild.ToString() + "\nCamX:" + camX.ToString() + "\nDevFileName:" + DevFileName + "\nDebugTime:" + ((double)(debugTime - debugStartTime) / 1000).ToString() + "s\n予想時間:" + SecondToTime((int)(player.x * 0.02)), GetColor(255, 255, 255));

            DrawBox(0, 0, player.hp, 20, GetColor(255, 0, 0), 1);
            DrawBox(0, 0, 255, 20, GetColor(255, 255, 255), 0);
        }
        /// <summary>
        /// 開発モード
        /// </summary>
        private static void Develop_Controller()
        {
            if (isDevelop)
            {
                for(int x = 0; x < 640; x++)
                {
                    DrawString(x * 50 - camX % 50, 0, ((camX - camX % 50 + x * 50) / 50).ToString(), GetColor(64, 64, 64));
                    DrawLine(x * 50 - camX % 50, 0, x * 50 - camX % 50, 480, GetColor(64, 64, 64));
                }
                for (int y = 0; y < 480 / player.height; y++)
                {
                    DrawLine(0, y * (int)player.height, 640, y * (int)player.height, GetColor(64, 64, 64));
                    DrawString(0, y * (int)player.height, y.ToString(),GetColor(64,64,64));
                }
                touchIndexDev = Touch_obj(0,new ObjType[] {ObjType.All});
                bool isp = false;
                if (CheckHitKey(KEY_INPUT_LCONTROL) == TRUE)
                {
                    if(CheckHitKey(KEY_INPUT_LSHIFT) == TRUE)
                    {
                        //追加
                        if(CheckHitKey(KEY_INPUT_A) == TRUE)
                        {
                            if(!IsShortcutPushed) ps.追加ToolStripMenuItem_Click();
                            isp = true;
                        }
                        //消去
                        if(CheckHitKey(KEY_INPUT_DELETE) == TRUE)
                        {
                            isp = true;
                        }
                        //保存
                        if (CheckHitKey(KEY_INPUT_S) == TRUE)
                        {
                            if (!IsShortcutPushed) ps.保存ToolStripMenuItem_Click();
                            isp = true;
                        }
                        //再読み込み
                        if (CheckHitKey(KEY_INPUT_R) == TRUE)
                        {
                            if (!IsShortcutPushed) ps.再読み込みToolStripMenuItem_Click();
                            isp = true;
                        }
                    }
                    //Resistを開く
                    if(CheckHitKey(KEY_INPUT_R) == TRUE)
                    {
                        if (!IsShortcutPushed) ps.registerToolStripMenuItem_Click();
                        isp = true;
                    }
                    //Propertyを開く
                    if(CheckHitKey(KEY_INPUT_P) == TRUE)
                    {
                        if (!IsShortcutPushed) ps.propertyToolStripMenuItem_Click();
                        isp = true;
                    }//保存
                    if(CheckHitKey(KEY_INPUT_S) == TRUE)
                    {
                        if (!IsShortcutPushed) ps.saveToolStripMenuItem_Click(new object(), new EventArgs());
                        isp = true;
                    }//開く
                    if(CheckHitKey(KEY_INPUT_O) == TRUE)
                    {
                        if (!IsShortcutPushed) ps.openToolStripMenuItem_Click(new object(), new EventArgs());
                        isp = true;
                    }
                }
                //選択する
                if(CheckHitKey(KEY_INPUT_Z) == TRUE)
                {
                    if(touchIndexDev != -1 && selectList.IndexOf(objList[touchIndexDev]) == -1)
                    {
                        selectList.Add(objList[touchIndexDev]);
                    }
                }
                //選択解除
                if(CheckHitKey(KEY_INPUT_X) == TRUE)
                {
                    selectList.Clear();
                }
                //選択中のオブジェクトを削除 And 削除
                if(CheckHitKey(KEY_INPUT_C) == TRUE)
                {
                    for(int i = 0; i < selectList.Count; i++)
                    {
                        removeList.Add(SelectObjToObjList(selectList[i]));
                    }
                    selectList.Clear();
                }

                //上
                if (CheckHitKey(KEY_INPUT_W) == TRUE)
                {
                    if (!isW)
                    {
                        isW = true;
                        foreach (var abc in selectList)
                            abc.y = Math.Max(abc.y - player.height, 0);
                    }
                }
                else isW = false;

                //下
                if (CheckHitKey(KEY_INPUT_S) == TRUE)
                {
                    if (!isA)
                    {
                        isA = true;
                        foreach (var abc in selectList)
                            abc.y = Math.Max(abc.y + player.height, 0);
                    }
                }
                else isA = false;

                //左
                if (CheckHitKey(KEY_INPUT_A) == TRUE)
                {
                    if (!isS)
                    {
                        isS = true;
                        foreach (var abc in selectList)
                            abc.x = Math.Max(abc.x - 50, 0);
                    }
                }
                else isS = false;

                //右
                if (CheckHitKey(KEY_INPUT_D) == TRUE)
                {
                    if (!isD)
                    {
                        isD = true;
                        foreach (var abc in selectList)
                            abc.x = Math.Max(abc.x + 50, 0);
                    }
                }
                else isD = false;

                //選択オブジェクトの選択中の表示
                foreach (var abc in selectList)
                {
                    DrawTriangle((int)abc.Cx, (int)abc.y, (int)abc.Cx + 10, (int)abc.y, (int)abc.Cx, (int)abc.y + 10, GetColor(255,0,0), 1);
                }

                IsShortcutPushed = isp;

                //オブジェクトの追加
                if (GetMouseInput() == 2)
                {
                    int BufPutX = (int)(player.x / 50) * 50;
                    int BufPutY = (int)((int)(player.y / player.height) * player.height);
                    if(BufPutX != PutX || BufPutY != PutY)
                    {
                        PutX = BufPutX;
                        PutY = BufPutY;
                        bool IsPut = true;
                        foreach(obj obj in objList)
                        {
                            if(obj.x == PutX && obj.y == PutY)
                            {
                                IsPut = false;
                                break;
                            }
                        }
                        if (IsPut)
                        {
                            obj o;
                            int AddNum = ps.GetListViewIndexNum();
                            if(AddNum == 0)
                            {
                                o = obj.Clone(ResistIndexOf(2));
                            }
                            else
                            {
                                o = obj.Clone(ResistIndexOf(AddNum));
                            }
                            o.x = PutX;
                            o.y = PutY;
                            objList.Add(o);
                        }
                    }
                }

                //オブジェクトの移動
                if(GetMouseInput() == 1)
                {
                    if (!IsHoldObj)
                    {
                        if (touchIndexDev != -1)
                        {
                            IsHoldObj = true;
                            HoldObj = objList[touchIndexDev];
                            ps.SetPsObj(HoldObj);
                        }
                    }
                    else
                    {
                        HoldObj.x = (int)(player.x / 50) * 50;
                        HoldObj.y = (int)((int)(player.y / player.height) * player.height);
                    }
                }
                else
                {
                    IsHoldObj = false;
                }

                //マウス位置取得とプレイヤー位置設定
                GetMousePoint(out posX, out posY);
                if (posY < 0) posY = 0;
                if (posY > 480) posY = 480;
                player.x = posX + obj.camX;
                player.y = posY;

                //カメラの位置を左
                IsClickArrow = false;
                if (CheckHitKey(KEY_INPUT_LEFT) == TRUE)
                {
                    camXPlus = Math.Max(1, camXPlus + 0.1);
                    obj.camX = Math.Max(obj.camX - (int)camXPlus, 0);
                    IsClickArrow = true;
                }
                //カメラの位置を右
                if (CheckHitKey(KEY_INPUT_RIGHT) == TRUE)
                {
                    camXPlus = Math.Max(1, camXPlus + 0.1);
                    obj.camX += (int)camXPlus;
                    IsClickArrow = true;
                }
                if (!IsClickArrow)
                {
                    camXPlus = 0;
                }
                //ファイル名を設定
                if (CheckHitKey(KEY_INPUT_F4) == TRUE)
                {
                     DevFileName = Interaction.InputBox("ファイル名を入力");
                }
            }

            //マップファイルを開く
            if (CheckHitKey(KEY_INPUT_F1) == TRUE)
            {
                mapFile = AppPath() + @"\map\" + DevFileName;
                ReadMapFile();
                ps.exitToolStripMenuItem_Click(new object(), new EventArgs());
                IsScroll = true;
                ScrollNum = 0;
                ScrollX = camX / 50;
                camX = 0;
                player.x = 0;
                debugStartTime = newTime;
                WaitTimer(500);
            }
            //開発モードに変更
            if (CheckHitKey(KEY_INPUT_F2) == TRUE)
            {
                for(int i = 1; i < objList.Count; i++)
                {
                    removeList.Add(objList[i]);
                }
                isDevelop = true;
                player.FitText("●");
                ps.Show();
            }
            //プレイモードに変更
            if (CheckHitKey(KEY_INPUT_F3) == TRUE)
            {
                ps.exitToolStripMenuItem_Click(new object(), new EventArgs());
            }
        }
        /// <summary>
        /// マップファイルを読み込む
        /// </summary>
        private static void ReadMapFile()
        {
            var op = obj.Clone(objList[0]);
            player = op;
            objList.Clear();
            objList.Add(op);

            int fontHeight = GetFontSize();
            using (var sr = new StreamReader(mapFile))
            {
                string st = sr.ReadLine();
                while (st != null)
                {
                    var objArr = new obj[480 / (int)player.height + 1];
                    var srArr = st.Split('/');

                    for (int i = 0; i < srArr.Count(); i++)
                    {
                        if (Convert.ToInt32(srArr[i]) != 0)
                        {
                            var o = obj.Clone(ResistIndexOf(Convert.ToInt32(srArr[i])));
                            o.y = i * fontHeight;
                            objArr[i] = o;
                        }
                    }

                    mapList.Add(objArr);

                    st = sr.ReadLine();
                }
            }
        }
        private static obj SelectObjToObjList(obj selectObj)
        {
            foreach (var o in objList)
            {
                if(o == selectObj)
                {
                    return o;
                }
            }
            return null;
        }
    }
}

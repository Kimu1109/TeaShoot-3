﻿#pragma warning disable CA1416

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static TeaShoot_3.Obj;
using static DxLibDLL.DX;
using Microsoft.VisualBasic;

namespace TeaShoot_3
{
    public partial class PropertyScreen : Form
    {

        public PropertyScreen()
        {
            InitializeComponent();
            PropertyScreen_Resize(new object(), new EventArgs());
        }

        private void propertyGrid1_Resize(object sender, EventArgs e)
        {
        }

        private void PropertyScreen_Resize(object sender, EventArgs e)
        {
            propertyGrid1.Width = (this.Width - 15) / 2;
            propertyGrid1.Height = this.Height - 15 - 15;

            listView1.Left = propertyGrid1.Width;
            listView1.Width = propertyGrid1.Width;
            listView1.Height = propertyGrid1.Height;
        }
        public void SetPsObj(Obj o)
        {
            propertyGrid1.SelectedObject = o;
        }
        private void PropertyScreen_Load(object sender, EventArgs e)
        {
            //ListView1のすべての列を自動調節
            foreach (ColumnHeader ch in listView1.Columns)
            {
                ch.Width = -2;
            }
            ReloadResist();
            DrawResist();
        }
        private void DrawResist()
        {
            listView1.Items.Clear();
            foreach (var l in resistList)
            {
                var li = new ListViewItem(l.num.ToString());
                li.SubItems.Add(l.text);
                listView1.Items.Add(li);
            }
        }
        public void propertyToolStripMenuItem_Click()
        {
            propertyGrid1.BringToFront();
        }

        public void registerToolStripMenuItem_Click()
        {
            DrawResist();
            listView1.BringToFront();
        }

        public void 追加ToolStripMenuItem_Click()
        {
            int maxNum = 0;
            foreach (var r in resistList) if (maxNum < r.num) maxNum = r.num;

            resistList.Add(new Obj(maxNum + 1, Obj.ObjType.Enemy, text: "(;-;)"));
            DrawResist();
        }

        private void objectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void 保存ToolStripMenuItem_Click()
        {
            foreach (var obj in resistList)
            {
                Obj.WriteObj(obj, Obj.AppPath() + @"\resist\" + obj.num + ".dat");
            }
            MessageBox.Show("finished!");
        }

        public void 再読み込みToolStripMenuItem_Click()
        {
            ReloadResist();
            DrawResist();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0) { return; }
            propertyGrid1.SelectedObject = resistList[listView1.SelectedIndices[0]];
            propertyGrid1.BringToFront();
        }

        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registerToolStripMenuItem_Click();
        }

        public void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var op = Obj.Clone(objList[0]);
            player = op;
            objList.Clear();
            objList.Add(op);

            int mapX = 0;
            int fontHeight = GetFontSize();
            using (var sr = new StreamReader(AppPath() + @"\map\" + DevFileName))
            {
                string st = sr.ReadLine();
                while (st != null)
                {
                    var srArr = st.Split('/');

                    for (int i = 0; i < srArr.Count(); i++)
                    {
                        if (Convert.ToInt32(srArr[i]) != 0)
                        {
                            var o = Obj.Clone(ResistIndexOf(Convert.ToInt32(srArr[i])));
                            o.y = i * fontHeight;
                            o.x = mapX * 50;
                            objList.Add(o);
                        }
                    }

                    mapX++;
                    st = sr.ReadLine();
                }
            }

            WaitTimer(500);
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists(Obj.AppPath() + @"\map\" + DevFileName))
            {
                File.Copy(Obj.AppPath() + @"\map\" + DevFileName, Obj.AppPath() + @"\backup\" + DevFileName + "." + rnd.NextInt64(0,99999999999999).ToString() + ".dat");
            }
            using (var sw = new StreamWriter(Obj.AppPath() + @"\map\" + DevFileName))
            {
                var writeList = new List<int[]>();
                int nowSpace = 0;
                int fontHeight = GetFontSize();
                int maxX = -1;

                foreach (var orz in objList)
                {
                    if (orz.x > maxX)
                    {
                        maxX = (int)orz.x;
                    }
                    if (orz.y < 0) orz.y = 0;
                    if (orz.y > 480) orz.y = 480;
                }

                while (nowSpace * 50 <= maxX)
                {
                    var YList = new int[480 / fontHeight + 1];
                    foreach (var o in objList)
                    {
                        if (o.num != 0)
                        {
                            if ((int)o.x == nowSpace * 50)
                            {
                                YList[(int)o.y / fontHeight] = o.num;
                            }
                        }
                    }
                    writeList.Add(YList);

                    nowSpace += 1;
                }
                foreach (var i in writeList)
                {
                    string writeStr = "";
                    foreach (var n in i)
                    {
                        writeStr += n.ToString() + "/";
                    }
                    writeStr = writeStr.TrimEnd('/');
                    sw.WriteLine(writeStr);
                }
            }
            MessageBox.Show("finished writing " + DevFileName);
        }

        public void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDevelop = false;
            player.FitText("_-=三(^o^)_旦");
        }
        public int GetListViewIndexNum()
        {
            if (listView1.SelectedIndices.Count == 0) { return 0; }
            return resistList[listView1.SelectedIndices[0]].num;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            保存ToolStripMenuItem_Click();
        }

        private void devFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevFileName = Interaction.InputBox("ファイル名を入力");
        }

        private void テキストを設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;

            using (var n = new TextDialog())
            {
                n.text = resistList[listView1.SelectedIndices[0]].text;
                if (n.ShowDialog() == DialogResult.OK)
                {
                    resistList[listView1.SelectedIndices[0]].FitText(n.text);
                    DrawResist();
                }
            }

        }

        private void テキストフィットToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            resistList[listView1.SelectedIndices[0]].FitText(resistList[listView1.SelectedIndices[0]].text);
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyToolStripMenuItem_Click();
        }

        private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            追加ToolStripMenuItem_Click();
        }

        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 再読み込みToolStripMenuItem_Click(object sender, EventArgs e)
        {
            再読み込みToolStripMenuItem_Click();
        }

        private void コードを編集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var n = new Form1())
            {
                n.code = resistList[listView1.SelectedIndices[0]].Code;
                if (n.ShowDialog() == DialogResult.OK)
                {
                    resistList[listView1.SelectedIndices[0]].Code = n.code;
                }
            }
        }

        private void codeRemoveを編集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var n = new Form1())
            {
                n.code = resistList[listView1.SelectedIndices[0]].CodeRemove;
                if (n.ShowDialog() == DialogResult.OK)
                {
                    resistList[listView1.SelectedIndices[0]].CodeRemove = n.code;
                }
            }
        }

        private void codeInitを編集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var n = new Form1())
            {
                n.code = resistList[listView1.SelectedIndices[0]].CodeInit;
                if (n.ShowDialog() == DialogResult.OK)
                {
                    resistList[listView1.SelectedIndices[0]].CodeInit = n.code;
                }
            }
        }
    }
}

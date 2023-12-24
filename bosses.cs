using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeaShoot_3.obj;
using static DxLibDLL.DX;
using Microsoft.VisualBasic;

namespace TeaShoot_3
{
    public static class bosses
    {
        public static void ProcessBoss1(int i)
        {
            var b1 = objList[i];

            var b1b = b1.boss1;

            switch (b1b.attack)
            {
                case boss1.attackType.MoveFirst:
                    if (Math.Sqrt(Math.Pow(b1.x - (640 - b1.width), 2) + Math.Pow(b1.y - (240 - b1.height / 2), 2)) <= 10)
                    {
                        b1b.nextAttack();
                    }
                    double angle = Math.Atan2(b1.y - (240 - b1.height / 2), b1.x - (640 - b1.width));

                    b1.x -= (float)Math.Cos(angle);
                    b1.y -= (float)Math.Sin(angle);

                    break;
                case boss1.attackType.Fishing:

                    if(b1b.y == 0)
                    {
                        b1.FitText(ReadAscii("boss1-fishing"));
                        b1b.y = -1;
                    }

                    b1b.x++;
                    if(b1b.x >= 100)
                    {
                        b1b.x = 0;
                        b1b.attackNum++;

                        var Rod = Clone(ResistIndexOf(15));
                        Rod.x = rnd.Next(0, 640);
                        Rod.y = -Rod.height;
                        Rod.remove = RemoveType.Big;

                        objList.Add(Rod);
                        if (b1b.attackNum > 30)
                        {
                            b1b.nextAttack();
                        }

                    }

                    break;
                case boss1.attackType.Kidding:

                    if (b1b.y == 0)
                    {
                        b1.FitText(ReadAscii("boss1-kidding"));
                        b1b.y = -1;
                    }

                    

                    break;
            }

        }
        public static void ProcessBoss2(int i)
        {
            var b2 = objList[i];

            var b2b = b2.boss2;

            switch (b2b.attack)
            {
                case boss2.attackType.MoveFirst:

                    if (Math.Sqrt(Math.Pow(b2.x - (640 - b2.width),2) + Math.Pow(b2.y - (240 - b2.height / 2),2)) <= 10)
                    {
                        b2b.nextAttack();
                    }
                    double angle = Math.Atan2(b2.y - (240 - b2.height / 2) , b2.x - (640 - b2.width));

                    b2.x -= (float)Math.Cos(angle);
                    b2.y -= (float)Math.Sin(angle);

                    break;
                case boss2.attackType.ShieldWall:
                    b2b.attackWait++;
                    if(b2b.attackWait >= 120)
                    {
                        b2b.attackWait = 0;

                        b2b.y++;
                        if (b2b.y >= 480 / player.height) b2b.y = 0;

                        for(int z = 0; z < 480 / player.height; z++)
                        {
                            if(b2b.y >= z - 3 && b2b.y <= z + 3)
                            {
                                var ao = Clone(ResistIndexOf(14));

                                ao.x = 640;
                                ao.y = z * player.height;
                                ao.speedX = -1;
                                ao.move = MoveType.BoundX0;

                                objList.Add(ao);
                            }
                        }

                        b2b.attackNum++;
                        if(b2b.attackNum > 20)
                        {
                            b2b.nextAttack();
                        }
                    }
                    break;
            }

        }


    }
    [Serializable]
    public class boss1
    {
        public attackType attack;
        public int attackWait;
        public int attackNum;
        public bool isRandom;

        public int x;
        public int y;

        public enum attackType
        {
            MoveFirst = 0,
            Fishing = 1,
            Kidding = 2,
            Punch = 3,
            NewDir = 4,
            BoundFish = 5,
            PunchPlus = 6,
            MoveLast = 7
        }
        public void nextAttack()
        {
            if (!isRandom)
            {
                if (attack == attackType.PunchPlus)
                {
                    isRandom = true;
                }
                else
                {
                    attack = (attackType)((int)attack + 1);
                    attackNum = 0;
                    attackWait = 0;
                    x = 0;
                    y = 0;
                }
            }
            if (isRandom)
            {
                attack = (attackType)obj.rnd.Next((int)attackType.Fishing, (int)attackType.PunchPlus);
                attackNum = 0;
                attackWait = 0;
                x = 0;
                y = 0;
            }
        }

    }

    [Serializable]
    public class boss2
    {
        public attackType attack;
        public int attackWait;
        public int attackNum;
        public bool isRandom;

        public int x;
        public int y;

        public enum attackType
        {
            MoveFirst = 0,
            ShieldWall = 1,
            CoffeeRain = 2,
            CoffeeBeam = 3,
            CoffeeBound = 4,
            CoffeeMore = 5,
            CoffeeSide = 6,
            CoffeeCycle = 7,
            CoffeeFire = 8,
            MoveLast = 9
        }
        public void nextAttack()
        {
            if(!isRandom)
            {
                if(attack == attackType.CoffeeFire) 
                {
                    isRandom = true;
                }
                else
                {
                    attack = (attackType)((int)attack + 1);
                    attackNum = 0;
                    attackWait = 0;
                }
            }
            if (isRandom)
            {
                attack = (attackType)obj.rnd.Next((int)attackType.ShieldWall, (int)attackType.CoffeeFire);
                attackNum = 0;
                attackWait = 0;
            }
        }
    }
}

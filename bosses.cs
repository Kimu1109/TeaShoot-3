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

            if (b1.boss1 == null)
            {
                b1.boss1 = new boss1();
                b1.boss1.attack = boss1.attackType.MoveFirst;
            }

            var b1b = b1.boss1;

            switch (b1b.attack)
            {
                case boss1.attackType.MoveFirst:

                    if((int)b1.x == 640 - b1.width && (int)b1.y == 480 - b1.height / 2)
                    {
                        b1b.nextAttack();
                    }
                    double angle = Math.Atan2(b1.x - (640 - b1.width), b1.y - (480 - b1.height / 2));

                    b1.x += (float)Math.Cos(angle);
                    b1.y += (float)Math.Sin(angle);

                    break;
                case boss1.attackType.ShieldWall:
                    b1b.attackWait++;
                    if(b1b.attackWait > 50)
                    {
                        b1b.attackWait = 0;

                        b1b.y++;
                        if (b1b.y >= 480 / player.height) b1b.y = 0;

                        for(int z = 0; z < 480 / player.height; z++)
                        {
                            if(b1b.y != z)
                            {
                                var ao = Clone(ResistIndexOf(14));

                                ao.x = 640;
                                ao.y = z * player.height;

                                objList.Add(ao);
                            }
                        }

                        b1b.attackNum++;
                        if(b1b.attackNum > 20)
                        {
                            b1b.nextAttack();
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

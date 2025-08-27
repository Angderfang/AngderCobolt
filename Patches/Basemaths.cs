using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Patches;
    public static class BasicSinCos
    {
        public static double InSin(double x)
        {
            return 1 - Math.Cos(x * Math.PI / 2);
        }

        public static double OutSin(double x)
        {
            return Math.Sin(x * Math.PI / 2);
        }
    public static double Wandertime = 1;
    public static bool turn = false;
    public static bool waiting = false;
    public static double waitingcooldown = 0;
    public static double waitingtimer = 0;
    public static double bounce = 0;
    public static double floatback = 0;
    public static double spin = 0;

    public static double Wander(double parts, double timetaken)
    {

        if (turn == false && waiting == false) //Walk forward
        {
            Wandertime += timetaken;
            if (Wandertime > 0)
            {
                waitingcooldown -= timetaken;
            }
        }
        if (turn == true && waiting == false)//Walk Back
        {
            Wandertime -= timetaken;
            if (Wandertime < parts - 15)
            {
                waitingcooldown -= timetaken;
            }
        }
        if (Wandertime <= 1 && Wandertime >= 0)
        {
            waiting = true;
            waitingtimer = waitingtimer + timetaken;
            if (waitingtimer >= 200)
            {
                waitingtimer = 0;
                waitingcooldown = 5;
                turn = false;
                waiting = false;
            }
            else if (waitingcooldown >= 1)
            {
                waitingtimer = 0;
                turn = false;
                waiting = false;
            }
        }
        else if (Wandertime >= parts - 16 && Wandertime <= parts - 15)
        {
            waitingtimer = waitingtimer + timetaken;
            waiting = true;
            if (waitingtimer >= 200)
            {
                waitingtimer = 0;
                waitingcooldown = 5;
                turn = true;
                waiting = false;
            }
            else if (waitingcooldown >= 1)
            {
                waitingtimer = 0;
                turn = true;
                waiting = false;
            }
        }
        else if(waiting == true || Wandertime >= parts - 16 && turn == false || Wandertime < 0 && turn == true) //Error checking
        {
            waiting = false;
            turn = turn == true ? false : true;
        }
        return Wandertime;
    }
    public static double Bounce(double timetaken)
    {
        if (waiting == false)
        {
            bounce = bounce + 0.1 * timetaken;
            if (bounce > 2)
                bounce = 0;
            return Math.Sin(-bounce * Math.PI / 2);
        }
        if (bounce > 2)
            bounce = 0;
        else if (bounce != 0)
            bounce = bounce + 0.1 * timetaken;
        return Math.Sin(-bounce * Math.PI / 2);
    }
    public static double Freefloat(double timetaken)
    {
        floatback = floatback - timetaken;
        return floatback * -0.1;
    }
    public static double Gentlespin(double timetaken)
    {
        spin = spin - timetaken;
        return spin * -0.1;
    }
}
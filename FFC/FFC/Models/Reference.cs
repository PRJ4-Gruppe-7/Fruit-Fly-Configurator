using System;
using System.Collections.Generic;
using System.Text;
using FFC.Services;

namespace FFC.Models
{
    public class Reference
    {

    #region Fields

    private int _id { get; set; }
    private uint _x { get; set; }
    private uint _y { get; set; }
    public int rssI1 { get; set; }
    public int rssI2 { get; set; }
    public int rssI3 { get; set; }

    #endregion


        #region Properties

        public uint X
    {
        get { return _x; }
        set
        {
            try
            {
                //Validates entry to make sure it isn't negative or extends 
                //maximum value of an integer
                _x = value < UInt32.MaxValue ? value : _x;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }
    }

    public uint Y
    {
        get { return _y; }
        set
        {
            try
            {
                //Validates entry to make sure it isn't negative or extends 
                //maximum value of an integer
                _y = value < UInt32.MaxValue ? value : _y;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }
    }

    public string Detail
    {
        get
        {
            return string.Format("x: {0,-8} y: {1,-8} rssi1: {2,-8} rssi2: {3,-8} rssi3: {4,-8}", X, Y, rssI1,
                rssI2, rssI3);
        }
    }

    public string ID
    {
        get { return string.Format($"ID: {_id}"); }
        set { _id = Int32.Parse(value); }
    }

    #endregion

    }
}

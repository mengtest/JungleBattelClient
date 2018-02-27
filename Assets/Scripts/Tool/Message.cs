using System;
using System.Linq;
using System.Text;
using Common;

public class Message
{
    /// <summary>
    /// 打包数据
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static byte[] PackData(RequestCode requestCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int) requestCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);

        return dataAmountBytes.Concat(requestCodeBytes).Concat(dataBytes).ToArray();
    }

    /// <summary>
    /// 打包数据
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int) requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int) actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + actionCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);

        return dataAmountBytes.Concat(requestCodeBytes).Concat(actionCodeBytes).Concat(dataBytes).ToArray();
    }

    private byte[] data = new byte[1024]; //存储数据的数组
    private int dataCount; //表示已经存储的数据个数

    public byte[] Data
    {
        get { return data; }
    }

    public int StartIndex
    {
        get { return dataCount; }
    }

    public int RemainSize
    {
        get { return data.Length - dataCount; }
    }

    private void AddCount(int count)
    {
        dataCount += count;
    }

    /// <summary>
    /// 解析读取数据
    /// </summary>
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallback)
    {
        AddCount(newDataAmount);

        while (true)
        {
            if (dataCount <= 4) return;
            int msgCount = BitConverter.ToInt32(data, 0);
            if (dataCount < msgCount + 4) return;

            //解析数据
            ActionCode actionCode = (ActionCode) BitConverter.ToInt32(data, 4);
            string msg = Encoding.UTF8.GetString(data, 8, msgCount - 4);

            processDataCallback(actionCode, msg);

            //更新数据个数，更新数组
            dataCount -= msgCount + 4;
            Array.Copy(data, msgCount + 4, data, 0, dataCount);
        }
    }
}
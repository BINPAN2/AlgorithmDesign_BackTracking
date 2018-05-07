using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 回溯算法计算最短时间
 *
 */

public class BackTrack
{
    private int[,] table = null;//[人员][任务]
    private int[] member = null;//人员分配表
    private List<List<int>> resultList = new List<List<int>>();//结果
    private int minTime = 100000;//最小用时
    private int curTime = 0;//当前用时

    /**
	 * 将人员分配到每个工作中去,所以任务是根据栈的层数来判明的，使用回溯的方法
	 */
    public void backTrack(int assign)
    {
        if (assign == table.GetLength(1) - 1)
        {//如果没有人可以分配了，则返回
            int paTime = table[member[assign],assign];//当前任务当前人员所用时间
            curTime += paTime;
            if (curTime < minTime)
            {
                minTime = curTime;
                resultList =  new List<List<int>>();
                resultList.Add(arr2Result(member));
            }
            else if (curTime == minTime)
            {
                resultList.Add(arr2Result(member));
            }
            curTime -= paTime;
            return;
        }
        else
        {
            for (int i = assign; i < table.GetLength(1); i++)
            {
                int paTime = table[member[i],assign];//当前任务当前人员所用时间
                curTime += paTime;
                if (curTime <= minTime)
                {//剪枝
                    swap(member, assign, i);
                    backTrack(assign + 1);
                    swap(member, i, assign);
                }
                curTime -= paTime;
            }
        }
    }

    /**
	 * 交换数组中两个数的位置
	 * @param arr
	 * @param i
	 * @param j
	 */
    private void swap(int[] arr, int i, int j)
    {
        int mid = arr[i];
        arr[i] = arr[j];
        arr[j] = mid;
    }

    /**
	 * 将数组转化为动态数组
	 * @param arr
	 * @return
	 */
    private List<int> arr2Result(int[] arr)
    {
        List<int> result = new List<int>();
        int temp;
        for (int i=0; i<arr.Length;i++)
        {
            temp = arr[i];
            result.Add(temp);
        }

        return result;
    }

    /**
	 * 获取table
	 * @param table
	 */
    public void setTable(int[,] table)
    {
        this.table = table;
        member = new int[table.GetLength(0)];
        for (int i = 0; i < table.GetLength(0); i++)
        {
            member[i] = i;
        }
    }

    /**
	 * 格式化输出数据
	 */
    public void display()
    {

            Console.WriteLine("最短用时：" + minTime);
            for (int i = 0; i < resultList.Count; i++)
            {
                Console.WriteLine("第" + (i + 1) + "种分配方式：");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                     Console.WriteLine("任务" + (j+1) + "分配给人员" + (resultList[i][j]+1) + ",用时" + table[resultList[i][j],j]);
                }
            }
    }
}
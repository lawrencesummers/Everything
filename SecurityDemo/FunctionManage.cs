using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecurityDemo
{
    public partial class FunctionManage : Form
    {
        public FunctionManage()
        {
            InitializeComponent();
        }

        private void FunctionManage_Load(object sender, EventArgs e)
        {
            //IList<Function> functions;
            //TreeListNode node = null;
            //using (var context = new EverythingContext())
            //{
            //    functions = context.Functions.ToList();
            //}

            //if (treeList.Nodes.Count != 0)
            // {
            //     treeList.ClearNodes();//首先清除所有的节点
            //}

            //foreach (var function in functions)  //使用foreach获取每一个functions集合中的对象
            //{

            //    if (function.ParentFunctionID == 0)//ParentFunctionID属性为0说明没有父节点，在获取对象集合的时候就已经得到了。
            //    {
            //        node = treeList.AppendNode(new object[] { function.FunctionID, function.FunctionName, function.Remark, function.ParentFunctionID }, -1);
            //    }
            //    else
            //    {
                    
            //    }

            //}

            
        }

        //private void AddChild(List<Function> childs, TreeListNode node)
        //{
        //    TreeListNode childnode;
        //    foreach (var child in childs)
        //    {

        //        childnode = treeList.AppendNode(new object[] { child.FunctionID, child.FunctionName, child.Remark, child.ParentFunctionID }, node);
        //        if (child.ParentFunctionID != 0)
        //        {

        //           // gettree(child, childnode);
        //        }

        //    }

        //}


        private void AppendToParent()
        {
            
        }
    }
}

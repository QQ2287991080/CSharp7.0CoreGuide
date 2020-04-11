using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chapter4
{
    /// <summary>
    /// 事件测试
    /// </summary>
    public class Event
    {
        public static void Invoker()
        {
            //{
            //    Stock stock = new Stock("Zero");
            //    stock.Price = 40;
            //    stock.changed += Stock_changed;
            //    stock.Price = 50;
            //}

            {
                StockZero stockZero = new StockZero("Zero");
                stockZero.Price = 30;
#if DEBUG
                stockZero.PriceCharged += Stock_changed;
#endif
#if TRACE
                stockZero.Price = 25;
#endif
            }
        }
        //[Conditional("DEBUG")]
        private static void Stock_changed(object sender, PriceChangedEventArgs e)
        {

            if ((e.newPrice - e.oldPrice)>0) Console.WriteLine("成功");
            else Console.WriteLine("失败"); 
        }
       

        //订阅者
        private static void Stock_changed(decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine($"老价格：{oldPrice},现价：{newPrice}");
        }
    }

    //广播者是包含委字段得类型，它通过调用委托决定何时广播
    //订阅者是方法的接受者，订阅者通过在广播者得委托上调用+=，-=来决定开始监听和结束监听
    #region 非标准事件
    //定义委托
    public delegate void ChangedHandler(decimal oldPrice,decimal newPrice);

    //public class broadcaster
    //{
    //    public event ChangedHandler changed;
    //}
    //广播者
    public class Stock
    {

        private string symbl;
        private decimal price;
        public Stock(string symbl)
        {
            this.symbl = symbl;
        }
       //时间声明
        public event ChangedHandler changed;//可以设置为null取消所有订阅者
        public decimal Price
        {
            get { return price; }
            set {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                if (changed != null)
                {
                    changed.Invoke(oldPrice,price);
                }
            }
        }

    }
    #endregion
    #region 标准时间模式
    /*
     .net 定义了一种标准模式，它的目的是为了保持框架的一致性。标准事件模式的核心是System.EventArgs类型，EventArgs是为事件传递信息的基类。可以通过继承它来传递信息。
     继承EventArgs之后需要遵循三条规则
     1.委托必须以void作为返回值
     2.委托必须接受两个参数，第一个参数是object类型，第二个是EventArgs的子类，第一个表明事件的广播者，第二参数包含了需要传递的额外信息
     3.委托的名称必须以EventHandler结尾
     事件的修饰符：和方法相似，事件可以是virtual，可以overridden，可以是abstract，可以是sealed，还有static的

         */

    public class PriceChangedEventArgs : System.EventArgs
    {
        public readonly decimal oldPrice;
        public readonly decimal newPrice;
        public PriceChangedEventArgs(decimal old,decimal n)
        {
            this.oldPrice = old;
            this.newPrice = n;

        }
    }

    public class StockZero
    {
        private string symbl;
        private decimal price;
        public StockZero(string symbl)
        {
            this.symbl = symbl;
        }

        public EventHandler<PriceChangedEventArgs> PriceCharged;//这个泛型委托满足三要素
        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceCharged?.Invoke(this, e);
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }
    }
    #endregion
    #region 时间的编译时解析
    /// <summary>
    /// 其实和普通的属性是非常之相像的，只是访问器的名字不同，当然功能也是有一些差异的
    /// </summary>
    public class Broadcaster
    {
        ChangedHandler handler;
        public event ChangedHandler Changed
        {
            add { handler += value; }
            remove { handler -= value; }
        }
    }
    #endregion
}

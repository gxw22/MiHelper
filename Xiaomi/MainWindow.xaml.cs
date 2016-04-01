using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xiaomi;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.Timers;

namespace Xiaomi
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 全局参数
        private static string cookieString = "xmuuid=XMGUEST-B309EF10-CADE-11E3-A6F9-4D5DFFE4B3AA; _ga=GA1.2.1750117577.1423330670; __utma=127562001.1750117577.1423330670.1423330670.1448116003.2; __utmz=127562001.1448116003.2.1.utmcsr=baidu|utmccn=(organic)|utmcmd=organic; xm_link_history=hYTw1WyqZzEm1%2F3woSAcwx5GHbRoxPRaLPtqUjVpROI%3D; xm_order_btauth=939625b60d06583c9f04adc421e9ca88; cUserId=iZkt8XwOCxw9Hx5-g6oQq8Nda8U; userId=46472706; device_id=pbw334EM4lYdUhqkxDxpY80iDpi5AaNZEeR8WkUGsbKOvbCToNjL1hhLHCMmEPsAb2F0TexomOjFPhl7tzdniTSzZ2BrevGFDB3DCELidW0tEMkVddWaRQLbz46NoqoD21fMr8Y6fO0ebleKlYj38Q51YVmXHXwzac8uwvX7uPNrJncHpZEmfV359J2lk7FhBPTGLENP; platform=P; xm_user_www_num=0; Hm_lvt_4982d57ea12df95a2b24715fb6440726=1457404015,1459389542; Hm_lpvt_4982d57ea12df95a2b24715fb6440726=1459407454; XM_46472706_UN=%E7%AB%BA%E5%8C%97%E9%A3%9E; msttime=http%3A%2F%2Fitem.mi.com%2F1154300033.html; msttime1=http%3A%2F%2Fitem.mi.com%2F1154300033.html; lastsource=s1.mi.com; mstuid=1430102050398_9950; xm_vistor=1430102050398_9950_1459475806744-1459475826044; mstz=1863416154b69eba-f29a402254b43892|%2F%2Fcart.mi.com%2Fcart%2Fadd%2F2154300020|598558979.110|pcpid|http%3A%2F%2Fwww.mi.com%2Findex.html|http%3A%2F%2Fwww.mi.com%2F";
        //private static string cookieString = "asdfghjklzxcvbnmqwertyuiop";
        private string jsonp = "111305094021714969559";
        private string serviceToken = "mD3NhY5ouA%2B0uzhzY22LxwDoU554zLpwv9Vy6cclrBWN5LqlRVF9dNCBYaUK3IXUspEvL6408Z2hZf3a36L5baIipfRPHelQ%2BVSfk2nr5lTfZMVrC2RRb0K6dXFhe2o1eQ4WQJ9VNpsvGj9Uv9fIuA%3D%3D";
        //private string productID = "2141800004"; //红米电信白
        //private string productID = "2140700031"; //红米1S电信金属灰
        //private string productID = "2141700001"; //路由
        //private string productID = "2141200012"; //note
        //private string productID = "2141800008"; //红米手机note移动增强版 白色
        //private string productID = "1142000007"; //小米电视2
        private string productID = "2143600001"; //小米4移动4G白
        private int timeout = 60000; //请求超时时间
        protected int loopStatus = 0;
        private int jumpWeb = 0;
        private bool popWeb = false;
        private string slabel1 = "";
        private string slabel10 = "";
        private string stextBox3 = "";
        private string stextBox2 = "";
        private string stextBox1 = "";
        private FileStream fs;
        private StreamWriter sw;
        private FileStream fsErr;
        private StreamWriter swErr;
        /// <summary>
        /// 抢购完成的次数
        /// </summary>
        private int trycount = 0;
        /// <summary>
        /// 允许使用的最大线程数
        /// </summary>
        //private int threadCount = 500;
        private List<Thread> tList = new List<Thread>();
        //private Thread[] t = new Thread[500];
        private int threadMaxID = 0;
        private Queue<object> tQueue = new Queue<object>();
        /// <summary>
        /// 抢购线程启动间隔
        /// </summary>
        private int threadInterval = 100;
        private bool tListCleaning = false;
        //private int[] threadState = new int[500];
        private delegate void noParaDelegate();
        private List<string> logBuffer0;
        private List<string> logBuffer1;
        /// <summary>
        /// loopState=0时日志数据的缓冲区
        /// </summary>
        private List<string> logBuffer2;
        private int bufferSize = 1024;
        /// <summary>
        /// 当前正在使用的未满的日志输出缓冲块编号
        /// </summary>
        private int currentBuffer = 0;
        /// <summary>
        /// 正在使用的缓冲区中已有的元素个数
        /// </summary>
        private int sCountInBuffer = 0;
        /// <summary>
        /// 抢购线程使用的timer
        /// </summary>
        System.Timers.Timer qTimer = new System.Timers.Timer();
        /// <summary>
        /// 验证码
        /// </summary>
        private string fk = "noUse";
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            textBox9.Text = "100";
            logBuffer0 = new List<string>();
            logBuffer1 = new List<string>();
            logBuffer2 = new List<string>();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_Vcode.Text))
            //{
            //    textBox1.Text = "未获取验证码，不能抢购";
            //    return;
            //}
            //else
            //{
            //    fk = txt_Vcode.Text.Trim();
            //}
            loopStatus = 1;
            trycount = 0;
            logBuffer0.Clear();
            logBuffer1.Clear();
            currentBuffer = 0;
            sCountInBuffer = 0;
            popWeb = false;
            //cookieString = ""; //测试用，正式使用时注释掉
            string filePathStr = "D:\\xiaomi" + retTime().ToString().Substring(6) + ".txt";
            if (!File.Exists(filePathStr))
            {
                fs = new FileStream(filePathStr, FileMode.Create, FileAccess.Write);//创建写入文件                 
            }
            else
            {
                fs = new FileStream(filePathStr, FileMode.Append, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            string fileErrPathStr = "D:\\ErrLog" + retTime().ToString().Substring(6) + ".txt";
            if (!File.Exists(filePathStr))
            {
                fsErr = new FileStream(fileErrPathStr, FileMode.Create, FileAccess.Write);//创建写入文件                 
            }
            else
            {
                fsErr = new FileStream(fileErrPathStr, FileMode.Append, FileAccess.Write);
            }
            swErr = new StreamWriter(fsErr);
            if (productID == "2140700031")
            {
                sw.WriteLine("红米电信版抢购：BM");
                label6.Content = "抢购：红米电信版";
            }
            else if (cnName(productID) == "未识别的型号")
            {
                sw.WriteLine("型号 " + productID + " 抢购：BM");
                label6.Content = "抢购：" + "型号 " + productID;
            }
            else
            {
                sw.WriteLine(cnName(productID) + "抢购：BM");
                label6.Content = "抢购：" + cnName(productID);
            }
            threadMaxID = 0;
            Thread tFile = new Thread(writeToFile);  //监视日志缓冲区并将日志写入文件的线程
            if (threadInterval < 40)
            {
                bufferSize = 2048;
            }
            else if (threadInterval < 100)
            {
                bufferSize = 1024;
            }
            else
            {
                bufferSize = 512;
            }
            tFile.Start();
            qTimer.Start();
            lbStatus.Content = "正在抢购中。。。";
        }
        /// <summary>
        /// 管理线程，定时使用新线程开始抢购
        /// </summary>
        public void StartThread()
        {
            while (loopStatus == 1)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Qiang));
                try
                {
                    if (tQueue.Count != 0)
                    {
                        int queueHead = Convert.ToInt32(tQueue.Dequeue());
                        t.Name = queueHead.ToString();
                        t.Start(queueHead);
                    }
                    else
                    {
                        t.Name = threadMaxID.ToString();
                        t.Start(threadMaxID);
                        threadMaxID++;
                    }
                    while (tListCleaning == true)
                    {
                        Thread.Sleep(10);
                    }
                    tList.Add(t);
                }
                catch (Exception ex)
                {
                    swErr.WriteLine(ex.Message + " " + +threadMaxID + " " + DateTime.Now.ToString("hh:mm:ss.fff"));
                    continue;
                }
                Thread.Sleep(threadInterval);
            }
        }

        public void Qiang(object threadID)
        {
            int tid = Convert.ToInt32(threadID);
            Random r = new Random(); //模拟随机延时
            string url = "";
            if (productID == "")
            {
                productID = "2140700031";
            }
            if (string.IsNullOrEmpty(fk)) //检测是否获取验证码
            {
                if (!string.IsNullOrEmpty(txt_Vcode.Text))
                {
                    fk = txt_Vcode.Text;
                }
                else
                {
                    stextBox1 = "未设置验证码";
                    updateUI();
                    return;
                }
            }
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StringBuilder sblog = new StringBuilder();
            sblog.Clear();
            Action uiAction = new Action(updateUI);
            sblog.Append("time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " thread:" + tid);
            url = "http://tp.hd.mi.com/hdget/cn?product=" + productID
                + "&addcart=1&m=1"
                //+ "&fk=" + fk
                + "&dnwte=" + DateTime.Now.ToString("MMddyyyy") + "1bc9f0"
                + "&jsonpcallback=cn" + productID
                + "&_=" + retTime().ToString();
                //+ "&ua=MozillaWindowsNTAppleWebKitKHTMLlikeGeckoChromeSafari";
            sblog.Append("\r\nRequest:");
            sblog.Append("\r\n" + url);
            stextBox3 = url.Substring(url.IndexOf("&_=") + 3, 13);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = HttpWebResponseUtility.DefaultUserAgent;
            request.Timeout = timeout;
            request.Headers.Add("Cookie", cookieString);
            //Thread.Sleep(62000);
            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[256];
                string bodyString = "";
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    bodyString += new String(read, 0, count);
                    count = readStream.Read(read, 0, 256);
                }
                sblog.Append("\r\n" + "Response:");
                sblog.Append("\r\n" + bodyString);
                int stopindex = bodyString.IndexOf("hdstop");
                string hdstop = bodyString.Substring(stopindex + 8, 4);
                stextBox2 = "hdstop:" + hdstop;
                if (hdstop == "true")
                {
                    loopStatus = 0;
                    qTimer.Stop();
                    stextBox1 = "抢购已结束,请停止日志";
                    sblog.Append("\r\n" + "抢购已结束" + DateTime.Now.ToString("hh:mm:ss.fff"));
                    addToBuffer(sblog.ToString());
                }
                else
                {
                    int urlindex = bodyString.IndexOf("hdurl");
                    int bigarc = bodyString.IndexOf("}");
                    string hdurl = bodyString.Substring(urlindex + 8, bigarc - 9 - urlindex);
                    //hdurl = "www.xiaomi.com";
                    if (hdurl.Length != 0)
                    {
                        stextBox1 = hdurl;
                        loopStatus = 0;
                        qTimer.Stop();
                        sblog.Append("\r\n" + "已抢到。");
                        slabel10 = "已抢到";
                        sblog.Append("\r\n" + hdurl);
                        //添加到购物车
                        url = "http://order.mi.com/cart/add/" + productID + "?";
                        url += "source=bigtap";
                        url += "&token=" + hdurl;
                        url += "&jsonpcallback=jQuery" + jsonp + "_" + (retTime() - 2).ToString();
                        url += "&_=" + retTime().ToString();
                        stextBox1 += "\n" + url;
                        sblog.Append("\r\n" + "RequestCart:");
                        sblog.Append("\r\n" + url);
                        HttpWebRequest reqeustCart = WebRequest.Create(url) as HttpWebRequest;
                        reqeustCart.Method = "GET";
                        reqeustCart.UserAgent = HttpWebResponseUtility.DefaultUserAgent;
                        reqeustCart.Timeout = 180000;
                        reqeustCart.Headers.Add("Cookie", cookieString);
                        HttpWebResponse responseCart = reqeustCart.GetResponse() as HttpWebResponse;
                        Stream receiveStreamCart = responseCart.GetResponseStream();
                        StreamReader readStreamCart = new StreamReader(receiveStreamCart, encode);
                        read = new Char[256];
                        bodyString = "";
                        count = readStreamCart.Read(read, 0, 256);
                        while (count > 0)
                        {
                            // Dumps the 256 characters on a string and displays the string to the console.
                            bodyString += new String(read, 0, count);
                            count = readStreamCart.Read(read, 0, 256);
                        }
                        sblog.Append("\r\n" + "Response:");
                        sblog.Append("\r\n" + bodyString);
                        //ResultWindow rw = new ResultWindow();
                        //rw.Show();
                        //rw.webBrowser1.Source = new Uri("http://www.mi.com");
                        //Process.Start("www.xiaomi.com");
                        //string date = DateTime.Now.ToString("yyMMdd");
                        string successUrl = "http://order.mi.com/event/success？goodsid=" + productID + "&_=" + retTime().ToString();
                        sblog.Append("\r\n" + "抢购成功跳转页面：" + successUrl);
                        if ((jumpWeb == 1)&&(popWeb == false))
                        {
                            Process.Start(successUrl);
                            popWeb = true; //存在多个线程同时抢到的情况，这种情况下只要弹一个网页
                        }
                    }
                    else
                    {
                        //stextBox1 = "空";
                    }
                }
                //loopStatus = 0; //测试时启用，保证只循环一次
                //Thread.Sleep(r.Next(60000) + 60000); //测试用，模拟随机延时
                slabel1 = "已完成第 " + trycount + " 次抢购";
                this.Dispatcher.Invoke(uiAction);
                trycount++;
                sblog.Append("\r\n" + "已完成第 " + trycount + " 次抢购  " + DateTime.Now.ToString("hh:mm:ss.fff"));
                addToBuffer(sblog.ToString());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                trycount++;
                sblog.Append("\r\n" + "Error:" + ex.Message + " 第 " + trycount + " 次抢购出错  " + DateTime.Now.ToString("hh:mm:ss.fff"));
                addToBuffer(sblog.ToString());
                slabel1 = " 第 " + trycount + " 次抢购出错  ";
                this.Dispatcher.Invoke(uiAction);
            }
            tQueue.Enqueue(tid);
            Thread tCurrent = tList.Find(delegate(Thread t) { return t.Name == tid.ToString(); });
            tList.Remove(tCurrent);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            loopStatus = 0;
            qTimer.Stop();
            textBox1.Text = "抢购手动结束，请停止日志";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DateTime t1 = new DateTime(1970, 1, 1);
            DateTime t2 = DateTime.Now;
            long a = (t2.Ticks - t1.Ticks) / 10000;
            textBox1.Text = a.ToString();
        }

        private long retTime()
        {
            DateTime t1 = new DateTime(1970, 1, 1);
            DateTime t2 = DateTime.Now;
            long a = (t2.Ticks - t1.Ticks) / 10000;
            return a;
        }
        /// <summary>
        /// 设置参数按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (loopStatus == 1)
            {
                return;
            }
            else if (comboBox1.SelectedItem == null)
            {
                //MessageBox.Show("请选择产品");
                textBox1.Text = "请选择产品";
            }
            else
            {
                threadInterval = Convert.ToInt32(textBox9.Text.Trim());
                qTimer.Interval = threadInterval;
                textBox1.Text = "";
                textBox7.Text = comboBox1.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(textBox4.Text.Trim()))
                {
                    cookieString = textBox4.Text.Trim();
                }
                if (!string.IsNullOrEmpty(textBox5.Text.Trim()))
                {
                    jsonp = textBox5.Text.Trim();
                }
                if (!string.IsNullOrEmpty(textBox6.Text.Trim()))
                {
                    if (cookieString.IndexOf("serviceToken") < 0)
                    {
                        cookieString += ";serviceToken=" + textBox6.Text.Trim();
                    }
                }
                else
                {
                    if (cookieString.IndexOf("serviceToken") < 0)
                    {
                        cookieString += ";serviceToken=" + serviceToken;
                    }
                }
                //if (!string.IsNullOrEmpty(textBox8.Text.Trim()))
                //{
                //    if (cookieString.IndexOf("xm_order_btauth") < 0)
                //    {
                //        cookieString += "; xm_order_btauth=" + textBox8.Text.Trim();
                //    }
                //}
                if (!string.IsNullOrEmpty(textBox7.Text.Trim()))
                {
                    productID = textBox7.Text.Trim();
                }
                if ((bool)checkBox1.IsChecked)
                {
                    jumpWeb = 1;
                }
                else
                {
                    jumpWeb = 0;
                }
                //if (string.IsNullOrEmpty(txt_Vcode.Text))
                //{
                //    textBox1.Text += "未获取验证码";
                //}
                //else
                //{
                //    fk = txt_Vcode.Text.Trim();
                //}
                label6.Content = "准备抢购：" + cnName(productID);
            }
            string userid = cookieString.Substring(cookieString.IndexOf("userId") + 7, 8);
            if (userid == "46472706")
            {
                label8.Content = "bbgy1222";
            }
            else if (userid == "8499977;")
            {
                label8.Content = "gxw22";
            }
            else
            {
                label8.Content = userid;
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (loopStatus == 1)
            {
                return;
            }
            if (sw.BaseStream != null)
            {
                closeFile();
                logBuffer0.Clear();
                logBuffer1.Clear();
                logBuffer2.Clear();
                textBox1.Text = "日志文件已关闭";
            }
            else
            {
                textBox1.Text = "日志文件已关闭";
            }
            lbStatus.Content = "抢购已停止";
        }

        private string cnName(string productID)
        {
            string cnName = "";
            ProductList plist = new ProductList();
            Product p = plist.First(delegate(Product pro) { return pro.ID == productID; });
            if (p != null)
            {
                cnName = p.Name;
            }
            else
            {
                cnName = "未定义的型号";
            }
            return cnName;
        }

        private void updateUI()
        {
            label1.Content = slabel1;
            textBox1.Text = stextBox1;
            textBox2.Text = stextBox2;
            textBox3.Text = stextBox3;
            if (slabel10 != "")
            {
                label10.Content = slabel10;
            }
        }

        private void getVcodeStatus(string str)
        {
            textBox1.Text = str;
        }
        /// <summary>
        /// 将日志写入文件，无缓冲
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="slog"></param>
        private void writeLog(StreamWriter sw, List<string> slog)
        {
            try
            {
                foreach (string s in slog)
                {
                    sw.WriteLine(s);
                }
            }
            catch(Exception ex)
            {
                swErr.WriteLine("writeLog:" + ex.Message);
            }
        }

        private void closeFile()
        {
            if (currentBuffer == 0)
            {
                bufferToFile(1);
                bufferToFile(0);
            }
            else
            {
                bufferToFile(0);
                bufferToFile(1);
            }
            sw.WriteLine("logBuffer2:");
            bufferToFile(2);
            sw.WriteLine("buffer write:" + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString());
            try
            {
                foreach (Thread t in tList)
                {
                    if (t.IsAlive == true)
                    {
                        sw.WriteLine("thread " + t.Name + " is still running");
                    }
                }
            }
            catch
            { }
            sw.Close();
            fs.Close();
            swErr.Close();
            fsErr.Close();
            sw.Dispose();
            fs.Dispose();
            swErr.Dispose();
            fsErr.Dispose();
        }

        private void addToBuffer(string s)
        {
            if (loopStatus == 1)
            {
                if (currentBuffer == 0)
                {
                    logBuffer0.Add(s);
                    sCountInBuffer = logBuffer0.Count;
                }
                if (currentBuffer == 1)
                {
                    logBuffer1.Add(s);
                    sCountInBuffer = logBuffer1.Count;
                }
                if (sCountInBuffer >= bufferSize)
                {
                    if (currentBuffer == 0)
                    {
                        currentBuffer = 1;
                    }
                    else
                    {
                        currentBuffer = 0;
                    }
                    sCountInBuffer = 0;
                }
            }
            else
            {
                logBuffer2.Add(s);
            }
        }
        /// <summary>
        /// 日志写入线程
        /// </summary>
        private void writeToFile()
        {
            int b0count = 0;
            int b1count = 0;
            //int sleepTime = (int)Math.Ceiling((double)(25000 / threadCount));
            //sw.WriteLine("thread sleep time: " + sleepTime);            
            while (loopStatus == 1)
            {
                Thread.Sleep(500);
                b0count = logBuffer0.Count;
                b1count = logBuffer1.Count;
                if (currentBuffer == 0)
                {
                    if (logBuffer1.Count >= bufferSize)
                    {
                        bufferToFile(1);
                    }
                }
                else
                {
                    if (logBuffer0.Count >= bufferSize)
                    {
                        bufferToFile(0);
                    }
                }
                sw.WriteLine("buffer thread run:" + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString());
                if ((logBuffer0.Count > 0) && (logBuffer1.Count > 0))
                {
                    sw.WriteLine("双缓冲出错,此次写入前缓冲区状态：");
                    sw.WriteLine("buffer0: " + b0count);
                    sw.WriteLine("buffer1: " + b1count);
                    sw.WriteLine("此次写入后缓冲区状态：");
                    sw.WriteLine("buffer0: " + logBuffer0.Count);
                    sw.WriteLine("buffer1: " + logBuffer1.Count);
                }
                ////附加任务：移除线程List中已结束的线程
                //if (tList.Count > 60000 / threadInterval)
                //{
                //    tListCleaning = true;
                //    Thread.Sleep(20);
                //    foreach (Thread t in tList)
                //    {
                //        if (t.IsAlive == false)
                //        {
                //            tList.Remove(t);
                //        }
                //    }
                //    tListCleaning = false;
                //}
            }
        }
        /// <summary>
        /// 将指定的缓冲区写入日志文件
        /// </summary>
        /// <param name="i"></param>
        private void bufferToFile(int i)
        {
            if (i == 0)
            {
                sw.WriteLine("buffer0:");
                foreach (string s in logBuffer0)
                {
                    sw.WriteLine(s);
                }
                logBuffer0.Clear();
            }
            else if (i == 1)
            {
                sw.WriteLine("buffer1:");
                foreach (string s in logBuffer1)
                {
                    sw.WriteLine(s);
                }
                logBuffer1.Clear();
            }
            else
            {
                sw.WriteLine("buffer2:");
                foreach (string s in logBuffer2)
                {
                    sw.WriteLine(s);
                }
                logBuffer2.Clear();
            }
            sw.WriteLine("buffer write:" + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            qTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            threadInterval = Convert.ToInt32(textBox9.Text.Trim());
            qTimer.Interval = threadInterval;
            textBox1.Text = "请设置参数";
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (loopStatus == 1)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Qiang));
                try
                {
                    if (tQueue.Count != 0)
                    {
                        int queueHead = Convert.ToInt32(tQueue.Dequeue());
                        t.Name = queueHead.ToString();
                        t.Start(queueHead);
                    }
                    else
                    {
                        t.Name = threadMaxID.ToString();
                        t.Start(threadMaxID);
                        threadMaxID++;
                    }
                    while (tListCleaning == true)
                    {
                        Thread.Sleep(10);
                    }
                    tList.Add(t);
                }
                catch (Exception ex)
                {
                    swErr.WriteLine(ex.Message + " " + +threadMaxID + " " + DateTime.Now.ToString("hh:mm:ss.fff"));
                }
            }
            else
            {
                qTimer.Stop();
            }
        }

        private void btn_GetVcode_Click(object sender, RoutedEventArgs e)
        {
            btn_GetVcode.IsEnabled = false;
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            string url = "http://tp.hd.mi.com/getmode/cn/?product=" + productID + "&jsonpcallback=getmode&_=" + retTime().ToString();
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = HttpWebResponseUtility.DefaultUserAgent;
            request.Timeout = 20000;
            request.Headers.Add("Cookie", cookieString);
            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[256];
                string bodyString = "";
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    bodyString += new String(read, 0, count);
                    count = readStream.Read(read, 0, 256);
                }
                int index = bodyString.IndexOf("{");
                string hint = bodyString.Substring(index);
                label_Hint.Content = hint;
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message + retTime().ToString();
            }
            finally
            {
                btn_GetVcode.IsEnabled = true;
            }
        }
    }
}

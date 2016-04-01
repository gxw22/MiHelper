using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Xiaomi
{
    public class Product
    {
        public string ID { set; get; }
        public string Name { set; get; }
    }

    public class ProductList : ObservableCollection<Product>
    {
        public ProductList()
        {
            //this.Add(new Product { ID = "2135000501", Name = "小米手机2S标准版" });
            //this.Add(new Product { ID = "2135000503", Name = "小米手机2S电信版" });
            //this.Add(new Product { ID = "2135000509", Name = "红米手机移动版 象牙白" });
            //this.Add(new Product { ID = "2141300060", Name = "红米手机移动版 纯白色" });
            //this.Add(new Product { ID = "2135200029", Name = "红米手机移动版 中国红" });
            //this.Add(new Product { ID = "2135000510", Name = "红米手机移动版 金属灰" });
            //this.Add(new Product { ID = "2135100067", Name = "红米手机联通版 金属灰" });
            //this.Add(new Product { ID = "2141600006", Name = "红米手机1S移动版 金属灰" });
            //this.Add(new Product { ID = "2142300025", Name = "红米手机1S移动版 糖果粉" });
            //this.Add(new Product { ID = "2142300024", Name = "红米手机1S移动版 柠檬黄" });            
            //this.Add(new Product { ID = "2141600007", Name = "红米手机1S联通版 金属灰" });
            //this.Add(new Product { ID = "2141900082", Name = "红米手机1S联通版 纯白色" });
            //this.Add(new Product { ID = "2141600007", Name = "红米手机1S联通版 金属灰" });
            //this.Add(new Product { ID = "2142300027", Name = "红米手机1S联通版 糖果粉" });
            //this.Add(new Product { ID = "2142300026", Name = "红米手机1S联通版 柠檬黄" });
            //this.Add(new Product { ID = "2140700031", Name = "红米手机1S电信版 金属灰" });
            //this.Add(new Product { ID = "2141800004", Name = "红米手机1S电信版 纯白色" });
            //this.Add(new Product { ID = "2141200012", Name = "红米 Note移动标准版 白色" });
            //this.Add(new Product { ID = "2141800008", Name = "红米手机note移动增强版 白色" });
            //this.Add(new Product { ID = "2140800013", Name = "小米手机3移动版(16GB)宝蓝色" });
            //this.Add(new Product { ID = "2135000522", Name = "小米手机3移动版(16GB)银灰色" });
            //this.Add(new Product { ID = "2135000520", Name = "小米手机3移动版(16GB)纯白色" });
            //this.Add(new Product { ID = "2140100007", Name = "小米手机3移动版(16GB)星空灰" });
            //this.Add(new Product { ID = "2135200028", Name = "小米手机3联通版(16GB)星空灰" });
            //this.Add(new Product { ID = "2140800022", Name = "小米手机3电信版(16GB)星空灰" });
            //this.Add(new Product { ID = "2140200002", Name = "小米手机3移动版(64GB)银灰色" });
            //this.Add(new Product { ID = "2140800006", Name = "小米手机3联通版(64GB)纯白色" });
            //this.Add(new Product { ID = "2140800007", Name = "小米手机3联通版(64GB)星空灰" });
            //this.Add(new Product { ID = "2141700001", Name = "小米路由器" });
            //this.Add(new Product { ID = "2141800011", Name = "小米盒子增强版" });
            //this.Add(new Product { ID = "1142000007", Name = "小米电视2" });
            //this.Add(new Product { ID = "2142300001", Name = "小米平板16GB" });
            //this.Add(new Product { ID = "2142700020", Name = "小米平板64GB" });            
            //this.Add(new Product { ID = "2141800009", Name = "红米手机note联通增强版 白色" }); 
            //this.Add(new Product { ID = "2143300001", Name = "红米手机1S移动4G版" });
            //this.Add(new Product { ID = "2143200006", Name = "红米 Note移动4G增强版 白色" });
            //this.Add(new Product { ID = "2143400005", Name = "红米 Note联通4G增强版 白色" });
            //this.Add(new Product { ID = "2143600001", Name = "小米手机4移动4G版 亮白16GB" });
            //this.Add(new Product { ID = "2144000012", Name = "小米手机4移动4G版 雅黑16GB" });
            //this.Add(new Product { ID = "2144100014", Name = "小米手机4移动4G版 雅黑64GB" });
            //this.Add(new Product { ID = "2144100013", Name = "小米手机4移动4G版 亮白64GB" });
            //this.Add(new Product { ID = "2143000004", Name = "小米手机4联通3G版 亮白16GB" });
            //this.Add(new Product { ID = "2143400001", Name = "小米手机4联通3G版 亮白64GB" });
            //this.Add(new Product { ID = "2143400004", Name = "小米手机4电信3G版 亮白16GB" });
            this.Add(new Product { ID = "2160100003", Name = "红米3时尚银白" });
            this.Add(new Product { ID = "2160100004", Name = "红米3时尚深灰" });
            this.Add(new Product { ID = "2160100005", Name = "红米3时尚金色" });
            this.Add(new Product { ID = "2160100006", Name = "红米3经典金色" });
            this.Add(new Product { ID = "2160700017", Name = "小米5高配黑色" });
            this.Add(new Product { ID = "2160700016", Name = "小米5高配白色" });
            this.Add(new Product { ID = "2143000001", Name = "小米手环" });
            this.Add(new Product { ID = "2141700014", Name = "小米路由mini" });
        }
    } 
}

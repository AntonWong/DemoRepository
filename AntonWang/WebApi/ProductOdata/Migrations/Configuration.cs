using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace ProductOdata.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProductsContext>
    {
        public Configuration()
        {
            //关闭自动生成迁移（让程序只打我们自己生成的迁移）
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductsContext context)
        {
            
        }
    }
}

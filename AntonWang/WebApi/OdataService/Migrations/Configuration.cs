using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace ProductOdata.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProductsContext>
    {
        public Configuration()
        {
            //�ر��Զ�����Ǩ�ƣ��ó���ֻ�������Լ����ɵ�Ǩ�ƣ�
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductsContext context)
        {
            
        }
    }
}

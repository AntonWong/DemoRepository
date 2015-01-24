using System;

namespace 设计模式
{
    public partial class  Employee
    {
        /*
         * 行为型-观察者模式、基于事件的观察者 Begin
         */
        public delegate void EmployeeChangedName(string changedName);

        private EmployeeChangedName employeeChangeName;
        public event EmployeeChangedName EmployeeChangedNameEvent
        {
            add
            {
                employeeChangeName += value;
            }
            remove { if (employeeChangeName != null) employeeChangeName -= value; }
        }

        public void ChangeName(string name)
        {
            Name = name;
            employeeChangeName(name);
        }

        static Func<EmployeeFactory.EmployeeCreateParameterContext, Employee.EmployeeAddress> addressFactory =
               context =>
               {
                   return new Employee.EmployeeAddress
                   {
                       Addess1 = context.AddressString.Split(',')[0],
                       Addess2 = context.AddressString.Split(',')[0]
                   };
               };
        /*
         * 行为型-观察者模式、基于事件的观察者 End
         */
        

        public class EmployeeAddress
        {
            public string Addess1 { get; set; }
            public string Addess2 { get; set; }
            
        }
        public EmployeeAddress AddressCollection { get; set; }
        public string Name { get; set; }
    }

    public class EmployeeFactory
    {
        public class  EmployeeCreateParameterContext
        {
            public string AddressString { get; set; }
        }

        public static Employee CreateEmployee(string name, string adderssStr,
            Func<EmployeeCreateParameterContext, Employee.EmployeeAddress> addressFactory)
        {
            var parameterContext = new EmployeeCreateParameterContext
            {
                AddressString = adderssStr
            };
            return new Employee
            {
                Name = name,
                AddressCollection = addressFactory(parameterContext)
            };
        }
        

    }

    public class InvokingClint
    {
        static readonly Func<EmployeeFactory.EmployeeCreateParameterContext, Employee.EmployeeAddress> addressFactory =
               context =>
               {
                   return new Employee.EmployeeAddress
                   {
                       Addess1 = context.AddressString.Split(',')[0],
                       Addess2 = context.AddressString.Split(',')[0]
                   };
               };

        public static void Method()
        {
           Employee employee = EmployeeFactory.CreateEmployee("HsutonWong", "北京,上海", addressFactory);
            employee.EmployeeChangedNameEvent += employee_EmployeeChangeEvent;
            employee.ChangeName("Hsuton......Wong");

            //Employee employee = EmployeeFactory.CreateEmployee("HsutonWong", "北京、上海", addressFactory);
        }

        static void employee_EmployeeChangeEvent(string changedName)
        {
            Console.WriteLine("修改后的名称：" + changedName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Address
    {
        public string ContactName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }

        public string PostalCode { get; set; }
    }

    public enum ShippingOptions
    {
        UPS = 100,
        FedEx = 200,
        Schenker = 300,
    }

    public class Order
    {
        public ShippingOptions ShippingMethod { get; set; }
        public Address Destination { get; set; }
        public Address Origin { get; set; }
    }

    //    public class ShippingCostCalculatorService
    //    {
    //        public double CalculateShippingCost(Order order)
    //        {
    //            switch (order.ShippingMethod)
    //            {
    //                case ShippingOptions.FedEx:
    //                    return CalculateForFedEx(order);
    //
    //                case ShippingOptions.UPS:
    //                    return CalculateForUPS(order);
    //
    //                case ShippingOptions.Schenker:
    //                    return CalculateForSchenker(order);
    //
    //                default:
    //                    throw new Exception("Unknown carrier");
    //
    //            }
    //
    //        }
    //
    //        double CalculateForSchenker(Order order)
    //        {
    //            return 3.00d;
    //        }
    //
    //        double CalculateForUPS(Order order)
    //        {
    //            return 4.25d;
    //        }
    //
    //        double CalculateForFedEx(Order order)
    //        {
    //            return 5.00d;
    //        }
    //    }

    public class ShippingCostCalculatorService
    {
        private readonly IPriceCalculator _calculator;

        public ShippingCostCalculatorService(IPriceCalculator calculator)
        {
            _calculator = calculator;
        }

        public double CalculateShippingCost(Order order)
        {
            return _calculator.Calculate(order);
        }
    }

    public class DefaultShipiingCostCalculator : IPriceCalculator
    {
        private readonly List<IShippingStrategy> _shippingStrategies;
        public DefaultShipiingCostCalculator()
        {
            _shippingStrategies = new List<IShippingStrategy>();

            _shippingStrategies.Add(new FedExShippingStrategy());
            _shippingStrategies.Add(new UpsCostStrategy());
            _shippingStrategies.Add(new SchenkerShippingStrategy());
        }
        public double Calculate(Order order)
        {
            return _shippingStrategies.First(x => x.IsMatch(order)).Calculate(order);
        }
    }

    public interface IPriceCalculator
    {
        double Calculate(Order order);
    }


    public interface IShippingStrategy
    {
        double Calculate(Order order);
        bool IsMatch(Order order);
    }

    public class FedExShippingStrategy : IShippingStrategy
    {
        public double Calculate(Order order)
        {
            return 5.00d;
        }

        public bool IsMatch(Order order)
        {
            return order.ShippingMethod == ShippingOptions.FedEx;
        }
    }

    public class UpsCostStrategy : IShippingStrategy
    {
        public double Calculate(Order order)
        {
            return 4.25d;
        }

        public bool IsMatch(Order order)
        {
            return order.ShippingMethod == ShippingOptions.FedEx;
        }
    }

    public class SchenkerShippingStrategy : IShippingStrategy
    {
        public double Calculate(Order order)
        {
            return 3.00d;
        }

        public bool IsMatch(Order order)
        {
            return order.ShippingMethod == ShippingOptions.FedEx;
        }
    }

}

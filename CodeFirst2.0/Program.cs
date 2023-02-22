using CodeFirst2._0.Models;
using CodeFirst2._0.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq;




/*
//Categories************************************************************************************************************************************************
Category weapons = new Category
{
    Name = "Weapons",
    Description = "Kills people",
    SubCategories = new List<Category>(),
    Products = new List<Product>(),
};

Category swords = new Category
{
    Name = "Swords",
    Description = "Stab people",
    ParentCategory = weapons,
    SubCategories = new List<Category>(),
    Products = new List<Product>(),
};
Category knives = new Category
{
    Name = "Knives",
    Description = "Nuthin personal bruv",
    ParentCategory = swords,
    Products = new List<Product>(),
};

Category blades = new Category
{
    Name = "Blades",
    Description = "No need to handle",
    ParentCategory = swords,
    Products = new List<Product>(),
};
swords.SubCategories.Add(knives);
swords.SubCategories.Add(blades);

Category guns = new Category
{
    Name = "Guns",
    Description = "shoot people",
    ParentCategory = weapons,
    SubCategories= new List<Category>(),
    Products = new List<Product>(),
};

Category subMachineGuns = new Category
{
    Name = "SubMachineChuns",
    Description = "Shoot a bit faster",
    ParentCategory = guns,
    Products = new List<Product>(),
};

Category machineGuns = new Category
{
    Name = "MachineGuns",
    Description = "So many bullets",
    ParentCategory = guns,
    Products = new List<Product>(),
};

Category pistols = new Category
{
    Name = "Pistols",
    Description = "Pea shooters",
    ParentCategory = guns,
    Products = new List<Product>(),
};

guns.SubCategories.Add(subMachineGuns);
guns.SubCategories.Add(machineGuns);
guns.SubCategories.Add(pistols);
//Products******************************************************************************************************************************************************
Product sword = new Product
{
    Name = "Orphan oblitaritor",
    Description = "The second worst thing to happen to them",
    Code = "Sub to Technoblade <3",
    Image = "bruh",
    Categories = new List<Category>
    {
        weapons,swords,blades
    },
    Variants = new List<ProductVariant>()
};

Product gunGun = new Product
{
    Name = "The GunGun",
    Description = "Gun but also a gun",
    Code = "Gunny",
    Image = "GunGun",
    Categories = new List<Category>
    {
        weapons,subMachineGuns
    },
    Variants = new List<ProductVariant>(),
};

Product bow = new Product
{
    Name = "Bow",
    Description = "No bullet",
    Code = "arrowBoiz",
    Image = "Bow",
    Categories = new List<Category>
    {
        weapons
    },
    Variants = new List<ProductVariant>(),
};

Product magnumOpus = new Product
{
    Name = "The Legend",
    Description = "Awesome gun",
    Code = "death",
    Image = "MagnumOpus",
    Categories = new List<Category>
    {
        weapons,pistols,machineGuns
    },
    Variants = new List<ProductVariant>(),
};

Product AK = new Product
{
    Name = "AK47",
    Description = "Probably best gun",
    Code = "Chep0Gun0",
    Image = "AK47",
    Categories = new List<Category>
    {
        weapons,subMachineGuns
    },
    Variants = new List<ProductVariant>(),
};

Product M16 = new Product
{
    Name = "M16",
    Description = "Freedom in physical form",
    Code = "MuricaYeah",
    Image = "M16",
    Categories = new List<Category>
    {
        weapons,subMachineGuns,machineGuns
    },
    Variants = new List<ProductVariant>(),
}; 
weapons.Products.Add(magnumOpus);
weapons.Products.Add(AK);
weapons.Products.Add(bow);
weapons.Products.Add(sword);
swords.Products.Add(sword);
blades.Products.Add(sword);
pistols.Products.Add(magnumOpus);
subMachineGuns.Products.Add(AK);
subMachineGuns.Products.Add(M16);
machineGuns.Products.Add(M16);
machineGuns.Products.Add(magnumOpus);
subMachineGuns.Products.Add(gunGun);

//Product variants ***********************************************************************************************************************
ProductVariant classicSword = new ProductVariant
{
    ParamName1 = "GG",
    ParamValue1 = "01",
    ParamName2 = "EZ",
    ParamValue2 = "02",
    ParamName3 = "LNAO",
    ParamValue3 = "03",
    Code = "DEATH",
    Price = 49,
    Product = sword,
    OrderLines = new List<OrderLine>(),
};
sword.Variants.Add(classicSword);

ProductVariant chromaAK = new ProductVariant
{
    ParamName1 = "GOLD",
    ParamValue1 = "01",
    ParamName2 = "SILVER",
    ParamValue2 = "02",
    ParamName3 = "WOOD",
    ParamValue3 = "03",
    Code = "Goggy",
    Price = 800,
    Product = AK,
    OrderLines = new List<OrderLine>(),
};
AK.Variants.Add(chromaAK);

ProductVariant flagM16 = new ProductVariant
{
    ParamName1 = "AMERICANFLAG",
    ParamValue1 = "01",
    ParamName2 = "AMERICANFLAG",
    ParamValue2 = "02",
    ParamName3 = "AMERICANFLAG",
    ParamValue3 = "03",
    Code = "MURICA",
    Price = 1600,
    Product = M16,
    OrderLines = new List<OrderLine>(),
};
M16.Variants.Add(flagM16);

ProductVariant legendGunGun = new ProductVariant
{
    ParamName1 = "Gun",
    ParamValue1 = "01",
    ParamName2 = "GunGun",
    ParamValue2 = "02",
    ParamName3 = "GunGunGunGun",
    ParamValue3 = "03",
    Code = "GunGunGunGunGunGun",
    Price = 9999999,
    Product = gunGun,
    OrderLines = new List<OrderLine>(),
};
gunGun.Variants.Add(legendGunGun);

ProductVariant maggieOpus = new ProductVariant
{
    ParamName1 = "Magnum",
    ParamValue1 = "01",
    ParamName2 = "Revolver",
    ParamValue2 = "02",
    ParamName3 = "Devolver",
    ParamValue3 = "03",
    Code = "bigBullet",
    Price = 4000,
    Product = magnumOpus,
    OrderLines = new List<OrderLine>(),
};
magnumOpus.Variants.Add(maggieOpus);

ProductVariant WoodenBOW = new ProductVariant
{
    ParamName1 = "WOOD",
    ParamValue1 = "01",
    ParamName2 = "Birch",
    ParamValue2 = "02",
    ParamName3 = "Spruce",
    ParamValue3 = "03",
    Code = "ArrowGoFly",
    Price = 10,
    Product = bow,
    OrderLines = new List<OrderLine>(),
};
bow.Variants.Add(WoodenBOW);
//***********************************************************************************************************************************************************************
Order swordOrder = new Order
{
    OrderState = State.CANCELLED,
    DateTime = DateTime.Now,
    Number = "number:1",
    OrderLines = new List<OrderLine>()
};

Order moreSwords= new Order
{
    OrderState = State.CANCELLED,
    DateTime = DateTime.Now,
    Number = "number:2",
    OrderLines = new List<OrderLine>()
};

Order AKORDER = new Order
{
    OrderState = State.DELIVERED,
    DateTime = DateTime.Now,
    Number = "number:1",
    OrderLines = new List<OrderLine>()
};

Order M16Orderor= new Order
{
    OrderState = State.DELIVERED,
    DateTime = DateTime.Now,
    Number = "number:1",
    OrderLines = new List<OrderLine>()
};

Order MagnumOpusOrder = new Order
{
    OrderState = State.DELIVERED,
    DateTime = DateTime.Now,
    Number = "number:1",
    OrderLines = new List<OrderLine>()
};

Order BowOrder = new Order
{
    OrderState = State.DELIVERED,
    DateTime = DateTime.Now,
    Number = "number:1",
    OrderLines = new List<OrderLine>()
};

Order GunGunOrder = new Order
{
    OrderState = State.NEW,
    DateTime = new DateTime(1999, 7, 15, 8, 30, 52),
    Number = "number:1",
    OrderLines = new List<OrderLine>()
};

//OrderLines***********************************************************************************************************************************************************
OrderLine AKBUY = new OrderLine
{
    Quantity = 4,
    Price = chromaAK.Price,
    ProductVariant = chromaAK,
    Order = AKORDER,
};
OrderLine MagnumOpusBUY = new OrderLine
{
    Quantity = 2,
    Price = maggieOpus.Price,
    ProductVariant = maggieOpus,
    Order = MagnumOpusOrder,
};
OrderLine SwordBuy = new OrderLine
{
    Quantity = 1,
    Price = classicSword.Price,
    ProductVariant = classicSword,
    Order = swordOrder
};

OrderLine SwordBuyer = new OrderLine
{
    Quantity = 2,
    Price = classicSword.Price,
    ProductVariant = classicSword,
    Order = swordOrder
};

OrderLine SwordieBYE= new OrderLine
{
    Quantity = 4,
    Price = classicSword.Price,
    ProductVariant = classicSword,
    Order = swordOrder
};

OrderLine anotherSwordLine = new OrderLine
{
    Quantity = 1,
    Price = classicSword.Price,
    ProductVariant = classicSword,
    Order = moreSwords
};

OrderLine M16Line = new OrderLine
{
    Quantity = 1,
    Price = flagM16.Price,
    ProductVariant = flagM16,
    Order = M16Orderor
};

OrderLine GunGunline = new OrderLine
{
    Quantity = 1,
    Price = legendGunGun.Price,
    ProductVariant = legendGunGun,
    Order = GunGunOrder
};

OrderLine BowLine = new OrderLine
{
    Quantity = 1,
    Price = WoodenBOW.Price,
    ProductVariant = WoodenBOW,
    Order = BowOrder
};
swordOrder.OrderLines.Add(SwordBuy);
swordOrder.OrderLines.Add(SwordBuyer);
swordOrder.OrderLines.Add(SwordieBYE);
moreSwords.OrderLines.Add(anotherSwordLine);
AKORDER.OrderLines.Add(AKBUY);
M16Orderor.OrderLines.Add(M16Line);
GunGunOrder.OrderLines.Add(GunGunline);
MagnumOpusOrder.OrderLines.Add(MagnumOpusBUY);
BowOrder.OrderLines.Add(BowLine);
//Customers ***************************************************************************************************************************************
Customer customer01 = new Customer
{
    City = "Prague",
    Street = "PoggyBoi",
    Zip = 694200,
    Name = "John",
    Surname = "Doe",
    Orders = new List<Order>()
    
};
customer01.Orders.Add(AKORDER);
customer01.Orders.Add(M16Orderor);
customer01.Orders.Add(BowOrder);
Customer customer02 = new Customer
{
    City = "NightCity",
    Street = "Broke",
    Zip = 64820,
    Name = "J0hny",
    Surname = "SilverHand",
    Orders = new List<Order>()

};
customer02.Orders.Add(MagnumOpusOrder);
customer02.Orders.Add(GunGunOrder);
Customer customer03 = new Customer
{
    City = "Murica",
    Street = "ChildSlayer",
    Zip = 50480,
    Name = "Technoblade",
    Surname = "THE BLADE",
    Orders = new List<Order>()
};
customer03.Orders.Add(swordOrder);
customer03.Orders.Add(moreSwords);

Customer customer04 = new Customer
{
    City = "Prague",
    Street = "peepee",
    Zip = 4545,
    Name = "Paulo",
    Surname = "ENDY",
};

Customer customer05 = new Customer
{
    City = "Prague",
    Street = "Peepe",
    Zip = 4545,
    Name = "Paulo",
    Surname = "MAC",
};
*/
//dBMethods.PrintCompleteCatalog();
//Console.WriteLine(dBMethods.GetNumberOfCustomersWhoCancelledOrder());
//dBMethods.PrintOrdersByCustomerLocations();
//dBMethods.PrintTopCustomers();
//dBMethods.GetMostPopularCategory();
//dBMethods.GetProductVariantsBoughtWith(21);

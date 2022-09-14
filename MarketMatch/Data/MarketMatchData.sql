USE [MarketMatch]

delete from SuperMarket
insert SuperMarket select 'Tesco', 'tesco_logo.png'
insert SuperMarket select 'Aldi', 'aldi_logo.png'


delete [Product]
insert [Product] select 1, 'Beans', 1, 200, 'Heinz', 'Tins'
insert [Product] select 2, 'Beans eggy', 1, 250, 'Eggs', 'Tins'
insert [Product] select 3, 'Poo', 4, 50, 'Jake', 'Sewerage'


delete ProductPrice
insert ProductPrice select 3, 1, getdate(), 3.4


delete ShoppingList
insert ShoppingList select 'be3e6997-04fe-4add-98ea-3170ccec4835', 1, 2
insert ShoppingList select 'be3e6997-04fe-4add-98ea-3170ccec4835', 2, 6
insert ShoppingList select '2', 1, 99
insert ShoppingList select '2', 3, 99

select * from SuperMarket
select * from Product
select * from ProductPrice
select * from ShoppingList




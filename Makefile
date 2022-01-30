.PHONY: order-prices product-customers customer-ranking test

all: task-1-order-prices task-2-product-customers task-3-customer-ranking

task-1-order-prices:
	dotnet run --project DevOpsInterview -- OrderPrices --orders-csv-file-path=orders.csv --products-csv-file-path=products.csv

task-2-product-customers:
	dotnet run --project DevOpsInterview -- ProductCustomers --orders-csv-file-path=orders.csv --products-csv-file-path products.csv

task-3-customer-ranking:
	dotnet run --project DevOpsInterview -- CustomerRanking --orders-csv-file-path=orders.csv --products-csv-file-path products.csv --customers-csv-file-path customers.csv

test:
	dotnet test
	
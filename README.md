### Quickstart for Docker

1. Start Ubuntu with docker:
    ```sh
    docker run -it ubuntu:21.10
    ```
2. Install and run all:
    ```sh
    apt update &&\
    apt install -y wget git make &&\
    wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb &&\
    dpkg -i packages-microsoft-prod.deb &&\
    apt update &&\
    apt install -y dotnet-sdk-5.0 &&\
    git clone https://github.com/revieve/devops-interview.git &&\
    git clone https://github.com/pbn4/devops-revieve.git &&\
    cp devops-interview/*.csv devops-revieve &&\
    cd devops-revieve &&\
    make all
    ```
3. Output CSV files should appear in current working directory

### Overview

1. Requirements:
    - .NET 5.0 SDK
    - Make

2. This is a CLI utility with ability to run 3 commands:
    - OrderPrices (Task1)
    - ProductCustomers (Task2)
    - CustomerRanking (Task3)

   To see help run:
   ```sh
   dotnet run --project DevOpsInterview
   ```

   Example run:
   ```sh
   dotnet run --project DevOpsInterview -- OrderPrices --orders-csv-file-path=orders.csv --products-csv-file-path=products.csv
   ```


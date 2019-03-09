1 - Criar pastas
    - Domain   
    - Shared
    - Tests

2 - Adicionar Solution
    - dotnet new sln

3 - Adentrar as pastas 'Domain' e 'Shared' e criar uma 'classlib'
    - dotnet new classlib

4 - Adentrar pasta Tests e criar o 'MSTEST'
    - dotnet new mstest 

5 - Adicionar o Domain, Shared e Tests à Solution (sln) 
    - dotnet sln add PaymentContext.Domain/PaymentContext.Domain.csproj
    - dotnet sln add PaymentContext.Shared/PaymentContext.Shared.csproj
    - dotnet sln add PaymentContext.Tests/PaymentContext.Tests.csproj

6 - Executar um Restore
    - dotnet restore (só para restaurar os pacotes etc)

7 - Executar um Build

Observações:
    /*
        Shared são itens que você pode compartilhar com seus domínios,
        nesse cenário, ele não precisaria nem existir, pois só temos um único domínios.
        Faria mais sentido se estivesse trabalhando com múltiplos domínios.

        O Domínio referencia o Shared, e somente ele.
        O Shared não referencia ninguém
        O Tests referencia o Domínio e o Shared
    */

8 - Adicionar referência do Shared no domínio
    - dotnet add reference ../PaymentContext.Shared/PaymentContext.Shared.csproj

9 - Adicionar referência do Shared no Tests
    - dotnet add reference ../PaymentContext.Shared/PaymentContext.Shared.csproj

10 - Adicionar referência do Domain no Tests
    - dotnet add reference ../PaymentContext.Domain/PaymentContext.Domain.csproj

11 - Executar um build

12 - Criação das classes BoletoPayment, CreditCardPayment, PayPalPayment em arquivos diferentes

13 - Criar os métodos para ficar de acordo com o SOLID 

14 - ValueObjects

15 - Adicionar Flunt    
    - dotnet add package flunt


Dependencias:

    Domain depende do shared
    Shared não depende de ninguém
    Tests depende de Domain e Shared

/*
    S O L I D   
    
    Single responsability Principle (SRP)
        Uma classe deve ter um, e apenas um, motivo para ser modificada

    Open/Close Principle (OCP)
        Entidades de Software (classes, módulos, funções, etc), devem estar
        abertas para extensão, mas fechadas para modificação

    Liskov Substitution Principle (LSP)
        Uma classe base deve poder ser substituída pela sua classe derivada

    Interface Segregation Principle (ISP)
        Clientes não devem ser forçados a depender de métodos que não usam

    Dependency Inversion Principle (DIP)
        Módulos de alto nível não devem depender de módulos de baixo nível.
        Ambos devem depender de abstrações; Abstrações não devem depender de detalhes.
        Detalhes devem depender de abstrações

        --Dependa de uma abstração e não de uma implementação

*/

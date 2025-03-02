#    📊 Big - Sistema de Gestão de Vendas e Pedidos

<p align="center">
  <img src="https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white">
  <img src="https://img.shields.io/badge/MudBlazor-0288D1?style=for-the-badge&logo=blazor&logoColor=white">
  <img src="https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white">
  <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white">

</p>


![Home](./image/Home.png)

## 📌 Descrição

Big é um sistema completo para gestão de vendas e pedidos, desenvolvido com **Blazor**, **MudBlazor**, **C#**, **MySQL** e **Docker**. Big é um sistema completo para gestão empresarial, oferecendo controle eficiente sobre clientes, vendedores e pedidos. Ele permite um acompanhamento detalhado das operações de venda, desde o cadastro de produtos até a emissão de relatórios gerenciais. Com um dashboard interativo, o sistema fornece gráficos e métricas estratégicas para análise de desempenho, auxiliando na tomada de decisão baseada em dados.

---

## ✨ **Funcionalidades Principais**

### 🔑 Autenticação e Controle de Acesso
- Login e logout com JWT
- Controle de permissões (Admin, Vendedor, etc.)

### 📦 Gestão de Produtos
- Cadastro, edição e remoção de produtos
- Filtro avançado por nome, categoria e preço

### 🛒 Gestão de Pedidos
- Registro e acompanhamento de pedidos
- Status do pedido (Aprovado, Rejeitado, Aguardando)
- Geração de notas e relatórios em PDF

### 📈 Dashboard Interativo
- Evolução de vendas ao longo dos meses
- Ticket médio por venda
- Pedidos aprovados, rejeitados e aguardando
- Produtos mais vendidos e vendedores mais ativos

---

## 🛠 **Tecnologias Utilizadas**

| Tecnologia       | Descrição |
|----------------|-----------|
| **Blazor** | Framework para construção do frontend |
| **MudBlazor** | Biblioteca de componentes UI para Blazor |
| **ASP.NET Core 8** | Backend robusto e escalável |
| **MySQL** | Banco de dados relacional |
| **Docker + Docker Compose** | Gerenciamento de infraestrutura |
| **QuestPDF** | Geração de relatórios em PDF |
| **Rate Limiting** | Proteção contra ataques DDoS |
| **xUnit** | Testes unitários para autenticação |

---

## 📸 Capturas de Tela

Aqui estão algumas capturas de tela do sistema, destacando suas principais funcionalidades.

---

### 📊 Dashboard
*Visão geral do sistema e métricas importantes.*  
![Dashboard](./image/Dashboard.png)

---

### 📂 Categorias
*Organize e gerencie os produtos com facilidade.*  
![Categorias](./image/Categorias.png)

---

### 🛒 Produtos
*Visualize, edite e gerencie os produtos do catálogo.*  
![Produtos](./image/Produtos.png)

---

### 📦 Pedidos
*Controle e acompanhe os pedidos em tempo real.*  
![Pedidos](./image/Pedidos.png)

---

### 📝 Nota de Pedido
*Registro detalhado das solicitações.*
<div style="border: 1px solid #ddd; padding: 10px; text-align: center;">
    <img src="./image/Nota.png" alt="Nota" width="60%">
</div>  

---

### 👥 Gerenciamento de Funcionários
*Controle de acessos e dados da equipe.*  
![Funcionarios](./image/Funcionarios.png)

---

### 🔐 Login e Autenticação
*Acesso seguro ao sistema.*  
![Login](./image/Login.png)

---


## 🚀 **Como Rodar o Projeto**

### 1️⃣ **Clonar o Repositório**
```sh
git clone https://github.com/seu-usuario/Big-System.git
cd Big-System
```

### 2️⃣ **Configurar o Banco de Dados**
```sh
docker-compose up -d
```

### 3️⃣ **Executar a Aplicação**
```sh
dotnet run --project Big.Server
```


## 📩 **Contato**
Caso tenha dúvidas ou sugestões, entre em contato:
- 📧 Email: lucasmagalhaes728@gmail.com
- 🔗 [LinkedIn](www.linkedin.com/in/lucas-magalhães-702684291)


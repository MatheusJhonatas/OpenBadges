# 🏅 OpenBadges — Plataforma de Emissão e Verificação de Badges
## Projeto arquitetado para implementação do padrão Open Badges, com foco em:
- Arquitetura limpa (Clean Architecture)
- Hexagonal (Ports & Adapters)
- DDD (Domain-Driven Design)
- Microserviços
- Boas práticas enterprise (.NET 9)

## 📌 Objetivo do Projeto
### Construir uma plataforma interoperável para:

- Gerenciar catálogo de badges (BadgeClass)
- Emitir badges (Assertion)
- Verificar badges publicamente
- Garantir conformidade com o padrão Open Badges

## 🏗 Arquitetura

### O projeto segue:

- Clean Architecture
- Hexagonal Architecture
- Separação por microserviço
- Domínio isolado de infraestrutura

## 📂 Estrutura Atual
OpenBadges  
│  
├── OpenBadges.sln  
├── src  
│   └── services  
│       └── badge-catalog  
│           ├── BadgeCatalog.Api  
│           ├── BadgeCatalog.Application  
│           ├── BadgeCatalog.Domain  
│           ├── BadgeCatalog.Ports   
│           └── BadgeCatalog.Adapters  
## 🧱 Estrutura Interna do Domain
BadgeCatalog.Domain  
│  
├── Aggregates  
│   └── BadgeClass.cs  
│  
├── ValueObjects  
│   ├── BadgeCriteria.cs  
│   └── BadgeImage.cs  
│  
├── Events  
├── Exceptions  
├── Enums  
## 🎯 Modelagem Atual
### ✅ Aggregate Root: BadgeClass
Representa a definição de um tipo de badge que pode ser emitido.

#### Propriedades atuais:

- Id
- Name
- Description
- Image (Value Object)
- Criteria (Value Object)
- IsActive

#### Garantias de domínio:

- Nome não pode ser vazio
- Descrição não pode ser vazia
- Image é obrigatório
- Criteria é obrigatório
- Encapsulamento com setters privados
- Preparado para EF Core

### ✅ Value Objects
- BadgeImage
-- Representa a URL da imagem do badge.
- BadgeCriteria
--Representa a descrição narrativa dos critérios para obtenção do badge.

## 🔗 Dependências entre camadas
### Arquitetura respeita a seguinte direção:
- Api → Application
- Application → Domain
- Adapters → Ports + Domain
- Domain → não depende de ninguém
- Ports → apenas contratos

## 🌿 Estratégia de Branch

- main → branch protegida
- develop → branch de desenvolvimento

## 🛠 Stack Atual

- .NET 9
- Clean Architecture
- DDD
- Git Flow básico

## 🚀 Próximos Passos

Modelagem do Aggregate Assertion

Introdução de Domain Events

Criação de Use Cases (Application Layer)

Persistência (EF Core)

Outbox Pattern

Mensageria

## 📖 Conformidade com Open Badges

O modelo está sendo desenvolvido com base no padrão Open Badges, preparando o sistema para:

Serialização futura em JSON-LD

Emissão verificável

Compatibilidade com ecossistema Open Badges

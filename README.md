# Gestão de Equipas

Este projeto é uma aplicação desktop desenvolvida em C# e WPF, inspirada no Football Manager. A aplicação fornece funcionalidades básicas de gestão de jogadores, treinos, jogos e táticas, armazenando os dados localmente em SQLite.

## Pré-requisitos

- **Sistema Operacional:** Windows 10 ou superior
- **.NET 6 SDK** com suporte a desktop/wpf
- **IDE:** Microsoft Visual Studio 2022 (ou compatível)
- **Base de Dados:** SQLite3

## Instruções para Configuração e Execução

1. **Clone o Repositório:**
   ```bash
   git clone https://github.com/esfolagatos/GestaoEquipas.git
   ```
2. **Abra o Projeto:**
   - Localize o arquivo `GestaoEquipas.sln` na raiz do repositório e abra-o no Visual Studio.
3. **Compilar:**
   - Selecione a configuração **Debug** e use `Build > Build Solution`.
4. **Executar:**
   - Pressione **F5** para iniciar a aplicação. Na primeira execução o arquivo `gestao_equipas.db` será criado automaticamente.

## Estrutura do Projeto

- **GestaoEquipas.Data** – camada de acesso a dados e criação das tabelas SQLite.
- **GestaoEquipas.Business** – serviços de negócio para manipulação de jogadores, treinos e jogos.
- **GestaoEquipas.UI** – aplicação WPF com janelas simples para gerir as entidades.

## Funcionalidades Implementadas

- **Gestão de Jogadores** – adicionar e listar jogadores.
- **Gestão de Treinos** – criar sessões de treino.
- **Gestão de Jogos** – registar jogos e resultados.
- **Editor Tático** – janela de demonstração para futuras funcionalidades.

## Licença

Distribuído sob a [Licença MIT](LICENSE).

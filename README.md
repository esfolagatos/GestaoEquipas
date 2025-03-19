# Gestão de Equipas

Este projeto é uma aplicação desktop desenvolvida em C# e WPF, inspirada no Football Manager, que permite:

- **Gestão de Atletas:** Registrar, editar, remover e visualizar estatísticas dos jogadores.
- **Gestão de Treinos e Exercícios:** Criar sessões de treino, arquivar exercícios personalizados e gerar fichas de treino com exportação em PDF.
- **Gestão de Jogos e Competições:** Calendarizar jogos e torneios, registrar resultados e calcular classificações automaticamente.
- **Editor Tático:** Criar e salvar formações e estratégias táticas, com interface gráfica para arrastar e posicionar jogadores.
- **Relatórios Estatísticos:** Analisar desempenho dos jogadores e da equipa com base em dados de jogos e treinos.

## Pré-requisitos

- **Sistema Operacional:** Windows 10 ou superior
- **.NET Framework:** 4.7.2 ou superior
- **IDE:** Microsoft Visual Studio (Community ou outra versão) com o workload ".NET desktop development"
- **Base de Dados:** SQLite3 (o projeto utiliza SQLite para armazenar dados)

## Instruções para Configuração e Execução

1. **Clone ou Baixe o Repositório:**
   - Para clonar via Git:
     ```bash
     git clone https://github.com/esfolagatos/GestaoEquipas.git
     ```
   - Ou faça o download do ZIP e extraia-o para uma pasta no seu computador.

2. **Abra o Projeto:**
   - Localize o arquivo de solução (`GestaoEquipas.sln`) na pasta extraída.
   - Abra o arquivo no Visual Studio.

3. **Compilar o Projeto:**
   - No Visual Studio, vá em **Build > Build Solution** para compilar o código e verificar se há erros.

4. **Executar a Aplicação:**
   - Após compilar, clique em **Start (F5)** para executar o programa.
   - A aplicação deverá iniciar com o menu principal onde podes navegar entre as funcionalidades.

5. **Configuração da Base de Dados (SQLite):**
   - O projeto está configurado para utilizar um arquivo de base de dados SQLite (por exemplo, `gestao_equipas.db`).
   - Se o arquivo não existir, a aplicação pode criá-lo automaticamente na primeira execução.

## Estrutura do Projeto

- **GestaoEquipas.Data:** Camada responsável pela conexão com o SQLite, criação de tabelas e operações de CRUD (Create, Read, Update, Delete).
- **GestaoEquipas.Business:** Lógica de negócio que implementa as regras de gestão de atletas, treinos, jogos e relatórios.
- **GestaoEquipas.UI:** Interface gráfica desenvolvida com WPF, contendo os menus e janelas para interação com o usuário.

## Funcionalidades

- **Gestão de Atletas:** Permite inserir, editar e remover jogadores, além de exibir estatísticas e histórico de assiduidade.
- **Gestão de Treinos e Exercícios:** Oferece um editor para criar exercícios, arquivá-los e montá-los em fichas de treino, com opção de exportação para PDF.
- **Gestão de Jogos e Competições:** Possibilita a calendarização de jogos, o registro de resultados e a geração automática de classificações.
- **Editor Tático:** Ferramenta gráfica para definir formações e estratégias táticas, com funcionalidade de arrastar e soltar ícones representando os jogadores.
- **Relatórios Estatísticos:** Geração de relatórios detalhados sobre o desempenho dos jogadores e da equipa com base nos dados coletados.

## Contribuições

Se tiveres dúvidas, sugestões ou quiser contribuir com o projeto:
- Abre um **issue** neste repositório.
- Envia um **pull request** com as melhorias.

## Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).

---

Esperamos que este projeto seja útil e que possas expandir suas funcionalidades conforme necessário. Se precisares de mais alguma ajuda, estou por aqui!

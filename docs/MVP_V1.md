# MVP v1 — FUTMAX (Futebol Europeu)

## 1) Funcionalidades fechadas para a versão 1

### 1.1 Utilizadores e segurança
- Login local com perfis: **Treinador Principal**, **Treinador Adjunto**, **Analista**, **Direção**.
- Controlo de permissões por ecrã (leitura, edição, exportação).

### 1.2 Atletas e plantel
- Registar atleta com dados-base: nome, data de nascimento, nacionalidade, posição principal/secundária, pé preferido, número da camisola.
- Estado desportivo: apto, lesionado, em recuperação, suspenso.
- Gestão de plantel por época (equipa principal, sub-23, sub-19).

### 1.3 Assiduidade
- Registo de presenças em treino e jogo.
- Marcação: presente, ausência justificada, ausência injustificada, atraso.
- Relatório por atleta e por período.

### 1.4 Jogos, competições e classificações
- Calendário de jogos (amigáveis e oficiais).
- Gestão de competições (liga, taça, fase de grupos, playoff).
- Classificação automática por competição com critérios: pontos, diferença de golos e golos marcados.

### 1.5 Treino e exercícios
- Planeamento de sessões de treino com data, objetivos e intensidade.
- Biblioteca de exercícios com etiquetas por objetivo (técnico/tático/físico/bola parada).
- Criação de ficha de treino agregando exercícios.
- Exportação de ficha para PDF e impressão.

### 1.6 Táticas (v1)
- Quadro tático 2D com campo e jogadores arrastáveis.
- Definição de formação (ex.: 4-3-3, 4-4-2, 3-5-2).
- Setas e zonas para instruções coletivas.
- Gravar e carregar modelos táticos.

## 2) Modelo de dados (base de dados)

> Stack atual: SQLite + camadas Data/Business/UI em C# (WPF).

### 2.1 Entidades nucleares
- `Player`
- `TrainingSession`
- `Exercise`
- `TrainingSheet`
- `AttendanceRecord`
- `Game`
- `Competition`
- `StandingRow`
- `TacticBoard`
- `TacticItem`

### 2.2 Esquema SQL proposto (MVP)

```sql
CREATE TABLE IF NOT EXISTS Player (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  FullName TEXT NOT NULL,
  BirthDate TEXT,
  Nationality TEXT,
  MainPosition TEXT,
  SecondaryPosition TEXT,
  PreferredFoot TEXT,
  ShirtNumber INTEGER,
  Squad TEXT,
  Status TEXT DEFAULT 'Apto'
);

CREATE TABLE IF NOT EXISTS Competition (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL,
  Type TEXT NOT NULL, -- Liga, Taca, Grupos, Playoff
  Season TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Game (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  GameDate TEXT NOT NULL,
  Opponent TEXT NOT NULL,
  CompetitionId INTEGER,
  IsHome INTEGER NOT NULL DEFAULT 1,
  GoalsFor INTEGER DEFAULT 0,
  GoalsAgainst INTEGER DEFAULT 0,
  Round TEXT,
  FOREIGN KEY (CompetitionId) REFERENCES Competition(Id)
);

CREATE TABLE IF NOT EXISTS TrainingSession (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  SessionDate TEXT NOT NULL,
  Objective TEXT,
  Intensity TEXT,
  Notes TEXT
);

CREATE TABLE IF NOT EXISTS Exercise (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Title TEXT NOT NULL,
  Category TEXT,
  DurationMinutes INTEGER,
  Material TEXT,
  Description TEXT
);

CREATE TABLE IF NOT EXISTS TrainingSheet (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  SessionId INTEGER NOT NULL,
  Title TEXT NOT NULL,
  PdfPath TEXT,
  CreatedAt TEXT NOT NULL,
  FOREIGN KEY (SessionId) REFERENCES TrainingSession(Id)
);

CREATE TABLE IF NOT EXISTS TrainingSheetExercise (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  TrainingSheetId INTEGER NOT NULL,
  ExerciseId INTEGER NOT NULL,
  SequenceOrder INTEGER NOT NULL,
  FOREIGN KEY (TrainingSheetId) REFERENCES TrainingSheet(Id),
  FOREIGN KEY (ExerciseId) REFERENCES Exercise(Id)
);

CREATE TABLE IF NOT EXISTS AttendanceRecord (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  PlayerId INTEGER NOT NULL,
  ContextType TEXT NOT NULL, -- Training/Game
  ContextId INTEGER NOT NULL,
  Status TEXT NOT NULL, -- Presente/Justificada/Injustificada/Atraso
  Notes TEXT,
  FOREIGN KEY (PlayerId) REFERENCES Player(Id)
);

CREATE TABLE IF NOT EXISTS TacticBoard (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL,
  Formation TEXT,
  CreatedAt TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS TacticItem (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  BoardId INTEGER NOT NULL,
  ItemType TEXT NOT NULL, -- Player, Arrow, Zone
  X REAL NOT NULL,
  Y REAL NOT NULL,
  Label TEXT,
  MetaJson TEXT,
  FOREIGN KEY (BoardId) REFERENCES TacticBoard(Id)
);
```

## 3) Estrutura do projeto (alinhada com o repositório)

- `GestaoEquipas.Data`
  - `Models/` (entidades)
  - `DataAccess/` (repositórios e inicialização SQLite)
- `GestaoEquipas.Business`
  - `Services/` (regras de negócio por domínio)
- `GestaoEquipas.UI`
  - `Views/` (janelas WPF)

## 4) Primeiras páginas/ecrãs a implementar no MVP

1. **Dashboard**
   - Resumo semanal: próximos jogos, treinos, atletas indisponíveis.
2. **Atletas**
   - Lista + formulário de registo/edição.
3. **Treinos**
   - Agenda de sessões + criação de ficha com exercícios.
4. **Jogos e Competições**
   - Calendário, detalhe de jogo, classificação.
5. **Assiduidade**
   - Grelha por sessão/jogo com marcação rápida.
6. **Táticas (Editor v1)**
   - Campo, formação base, objetos gráficos, gravação de modelo.

## 5) Critérios de aceitação do MVP
- CRUD funcional para Atletas, Treinos, Exercícios e Jogos.
- Classificação automática para competições de liga.
- Exportação de ficha de treino para PDF.
- Editor tático com gravação/carregamento de pelo menos 1 modelo.
- Dados persistidos em SQLite com migração inicial automática.

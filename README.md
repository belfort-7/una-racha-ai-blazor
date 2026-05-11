# RachaAí — Blazor 💸

Implementação do Dashboard do aplicativo **RachaAí** em **C# / Blazor (.NET 8)**, desenvolvida para a disciplina de **Interação Humano-Computador e UX** — Lista de Exercícios XVI.  
Centro Universitário UNA · Professor Daniel Henrique Matos de Paiva

---

## 👤 Nome do Aluno / Equipe

Joao Victor Belfort Sousa Castro 

---

## 🚀 Como rodar o projeto

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/ihcux-racha-ai-blazor.git
cd ihcux-racha-ai-blazor

# 2. Restaure os pacotes
dotnet restore

# 3. Execute o projeto
dotnet run

# 4. Acesse no navegador
# http://localhost:5000/dashboard
```

---

## 📁 Estrutura do Projeto

```
RachaAiBlazor/
├── Models/
│   └── Grupo.cs                        ← Modelo de dados dos grupos
├── Components/
│   ├── Pages/
│   │   └── Dashboard.razor             ← Página principal (@page "/dashboard")
│   ├── Shared/
│   │   └── GrupoCard.razor             ← Componente reutilizável de grupo
│   └── Layout/
│       ├── MainLayout.razor
│       └── NavMenu.razor               ← Navegação com link para o Dashboard
└── wwwroot/
    └── rachaai.css                     ← Estilos customizados do RachaAí
```

---

## 🧩 Componentes Implementados

### `Models/Grupo.cs`
Modelo de dados com as propriedades exigidas:
- `Nome` — nome do grupo
- `Categoria` — tipo (Casa, Lazer, Viagem, Contas, Mercado)
- `ValorPendente` — valor em `decimal`
- `NoVermelho` — `true` = eu devo / `false` = eu recebo
- `IconeCategoria` — propriedade computada que retorna o emoji correto via `switch expression`

### `Components/Shared/GrupoCard.razor`
Componente reutilizável que recebe um `[Parameter] Grupo` e exibe:
- Nome em negrito
- Ícone/emoji da categoria
- Valor formatado em Real (`R$ 0.000,00`)
- Cor vermelha (`text-danger`) se o usuário deve, verde (`text-success`) se recebe
- Borda lateral colorida para reforço visual da heurística de Visibilidade

### `Components/Pages/Dashboard.razor`
Página principal com rota `@page "/dashboard"` contendo:
- **Header** com saudação e botão FAB "Novo Gasto"
- **3 Cards de Resumo**: Saldo Geral, A Receber, A Pagar (calculados por LINQ)
- **Campo de Busca** com `@bind-value:event="oninput"` para filtro em tempo real ⭐
- **Lista de Grupos** via `@foreach` + componente `<GrupoCard />`
- **Estado Vazio** com mensagem amigável quando não há grupos
- **Toast de Feedback** após clicar em "Novo Gasto"

---

## 🎨 Implementação Blazor — Da Hierarquia Visual do Miro ao Código

No protótipo de baixa fidelidade (Lista XV), a hierarquia visual foi construída com:
- **Contraste de peso** (preto forte = urgente, cinza = neutro)
- **Separação espacial** (você deve em cima, te devem embaixo)
- **Tipografia** (valores grandes, labels pequenos)

Na implementação Blazor, essa lógica foi transposta assim:

| Decisão no Miro | Como ficou no Blazor |
|---|---|
| Saldo grande e destacado no topo | `stat-card--saldo` com `font-size: 1.45rem` e `border-left` preta |
| Vermelho = você deve | Classe CSS `.grupo-card--deve` + `text-danger` no valor |
| Verde = você recebe | Classe CSS `.grupo-card--recebe` + `text-success` no valor |
| Hierarquia de texto (label pequeno + valor grande) | `.stat-card__label` (0.78rem) + `.stat-card__valor` (1.45rem) |
| Botão FAB de destaque | `.btn-fab` arredondado, preto, com `box-shadow` e `hover` elevado |
| Estado vazio amigável | `@if (!GruposFiltrados.Any())` com mensagem e botão de ação |

---

## 🔧 Maior Dificuldade Técnica — Componentização do `GrupoCard`

O maior desafio foi **passar dados e estilos dinâmicos entre o componente pai e o filho** de forma elegante.

**Problema:** A classe CSS do card (`grupo-card--deve` ou `grupo-card--recebe`) depende da propriedade `NoVermelho` do objeto `Grupo`, que vive no pai (`Dashboard.razor`). No início, a tentação é colocar essa lógica no pai — mas isso quebra o encapsulamento do componente.

**Solução adotada:** A lógica de qual classe aplicar ficou **dentro do próprio `GrupoCard.razor`**, usando interpolação inline:

```razor
<div class="grupo-card @(Grupo.NoVermelho ? "grupo-card--deve" : "grupo-card--recebe")">
```

Isso mantém o componente **autocontido**: quem usa o `GrupoCard` não precisa saber nada sobre CSS — só passa o objeto `Grupo` como `[Parameter]` e o componente cuida de tudo. Essa é a essência da componentização bem feita em Blazor.

---

## ⭐ Desafio Extra — Busca em Tempo Real

Implementado com `@bind-value:event="oninput"` para que o filtro ocorra a cada tecla digitada, sem necessidade de submeter um formulário:

```razor
<input type="text"
       @bind-value="termoBusca"
       @bind-value:event="oninput"
       placeholder="Buscar grupo ou categoria..." />
```

A lista filtrada é calculada por uma propriedade computada:

```csharp
private IEnumerable<Grupo> GruposFiltrados =>
    string.IsNullOrWhiteSpace(termoBusca)
        ? grupos
        : grupos.Where(g =>
            g.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase) ||
            g.Categoria.Contains(termoBusca, StringComparison.OrdinalIgnoreCase));
```

---

## 🎯 Heurística de Nielsen Aplicada

**#1 — Visibilidade do Status do Sistema**

- O saldo geral é exibido imediatamente ao carregar a página, antes de qualquer scroll
- Cards de resumo (A Receber / A Pagar) usam cores semânticas: verde e vermelho
- A borda lateral colorida de cada `GrupoCard` comunica o status sem que o usuário precise ler o texto
- Toast de feedback ao clicar em "Novo Gasto" confirma que a ação foi registrada

---

## 🔗 Repositório do Protótipo (Lista XV)

[github.com/seu-usuario/ihcux-racha-ai](https://github.com/seu-usuario/ihcux-racha-ai)

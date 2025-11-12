# API REST com ASP.NET Core - Guia de Referência

## 📋 Sobre o Projeto

Este projeto foi desenvolvido durante um curso de ASP.NET Core focado na criação de APIs RESTful. O objetivo principal foi aprender os fundamentos de comunicação HTTP, operações CRUD, e boas práticas no desenvolvimento de APIs.

## 🎯 Conceitos Principais Aprendidos

### 1. Controllers e Rotas

Os **Controllers** são responsáveis por abrir a comunicação entre a aplicação e o mundo externo (clientes HTTP).

**Conceitos importantes:**
- Definição de rotas para os endpoints da API
- Extensão da classe base de controllers
- Utilização do construtor para **Injeção de Dependência**
- Criação de **Actions** (métodos que respondem às requisições HTTP)

```csharp
[ApiController]
[Route("api/[controller]")]
public class MeuController : ControllerBase
{
    private readonly MeuServico _servico;
    
    // Injeção de dependência pelo construtor
    public MeuController(MeuServico servico)
    {
        _servico = servico;
    }
}
```

### 2. Verbos HTTP e Operações CRUD

#### **HttpPost** - Criar Recursos
Utilizado para criar novos recursos no sistema.

```csharp
[HttpPost]
public IActionResult CriarRecurso([FromBody] RecursoDto dto)
{
    // Lógica de criação
    return CreatedAtAction(nameof(ObterPorId), new { id = novoId }, recurso);
}
```

#### **HttpGet** - Recuperar Dados
Utilizado para buscar informações do sistema.

```csharp
[HttpGet]
public IActionResult ListarTodos()
{
    // Retorna lista de recursos
}

[HttpGet("{id}")]
public IActionResult ObterPorId(int id)
{
    // Retorna um recurso específico
}
```

**Paginação:** Implementada para evitar retornar muitas informações de uma vez, melhorando performance e experiência do usuário.

```csharp
[HttpGet]
public IActionResult Listar([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
{
    var resultado = _servico.ObterPaginado(pagina, tamanhoPagina);
    return Ok(resultado);
}
```

#### **HttpPut** - Atualização Completa
Utilizado para atualizar um recurso completo no sistema, substituindo todos os dados.

```csharp
[HttpPut("{id}")]
public IActionResult AtualizarCompleto(int id, [FromBody] RecursoDto dto)
{
    // Busca o recurso pelo ID
    // Atualiza todas as propriedades
    return NoContent();
}
```

#### **HttpPatch** - Atualização Parcial
Utilizado para atualizar apenas campos específicos de um recurso, sem afetar outros dados.

```csharp
[HttpPatch("{id}")]
public IActionResult AtualizarParcial(int id, [FromBody] JsonPatchDocument<RecursoDto> patchDoc)
{
    // Atualiza apenas os campos especificados no JSON
    return NoContent();
}
```

**Vantagem do PATCH:** Permite alterar apenas o necessário sem precisar enviar todo o objeto.

#### **HttpDelete** - Deletar Recursos
Utilizado para remover recursos do sistema.

```csharp
[HttpDelete("{id}")]
public IActionResult Deletar(int id)
{
    // Busca o recurso
    // Remove usando Entity Framework
    return NoContent();
}
```

### 3. Entity Framework Core

Utilizado para comunicação com o banco de dados de forma simplificada através de ORM (Object-Relational Mapping).

**Operações realizadas:**
- Consultas ao banco de dados
- Inserção de novos registros
- Atualização de registros existentes
- Remoção de registros

```csharp
// Exemplo de operações com EF Core
var entidade = await _context.MinhaEntidade.FindAsync(id);
_context.MinhaEntidade.Add(novaEntidade);
_context.MinhaEntidade.Update(entidadeAtualizada);
_context.MinhaEntidade.Remove(entidade);
await _context.SaveChangesAsync();
```

### 4. DTOs (Data Transfer Objects)

**DTOs** são objetos utilizados para transferência de dados entre camadas da aplicação, separando a representação de dados da estrutura do banco.

**Tipos de DTOs implementados:**

- **DTO de Criação:** Contém apenas os campos necessários para criar um recurso
- **DTO de Leitura:** Contém os dados que serão retornados ao cliente, podendo incluir informações calculadas em tempo de execução
- **DTO de Atualização:** Contém os campos que podem ser atualizados

**Vantagens:**
- Controle sobre quais dados são expostos
- Possibilidade de incluir dados calculados que não existem no banco
- Validação específica para cada operação
- Segurança (evita exposição de dados sensíveis)

```csharp
// DTO de Criação
public class CriarRecursoDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}

// DTO de Leitura
public class LerRecursoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    // Propriedade calculada em tempo de execução
    public string Status { get; set; }
}
```

### 5. Configuração da Aplicação

#### appsettings.json
Arquivo de configuração utilizado para armazenar informações que podem variar entre ambientes (desenvolvimento, homologação, produção).

**Informações comumente armazenadas:**
- Strings de conexão com banco de dados
- Configurações de serviços externos
- Parâmetros da aplicação
- Níveis de log

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MinhaDB;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ConfiguracoesApp": {
    "TamanhoPaginaPadrao": 10
  }
}
```

**Carregamento das configurações:**
- Feito em tempo de inicialização da aplicação
- Acessível através de injeção de dependência
- Permite alteração sem recompilar o código

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core (Web API)
- Entity Framework Core
- Banco de dados local
- DTOs para transferência de dados
- JSON para serialização/deserialização

## 📚 Boas Práticas Implementadas

1. **Separação de responsabilidades** com Controllers, Services e Repositories
2. **Uso de DTOs** para controlar a entrada e saída de dados
3. **Injeção de Dependência** para baixo acoplamento
4. **Paginação** para otimização de performance
5. **Verbos HTTP apropriados** para cada tipo de operação
6. **Configurações externalizadas** via appsettings.json
7. **Atualização parcial** com PATCH para operações mais eficientes

## 🎓 Principais Aprendizados

- Como estruturar uma API RESTful seguindo convenções
- Diferença entre PUT (atualização completa) e PATCH (atualização parcial)
- Importância da paginação em endpoints que retornam listas
- Como usar DTOs para criar camadas de abstração
- Configuração e uso do Entity Framework Core
- Organização de configurações com appsettings.json

## 🔍 Referência Rápida de Verbos HTTP

| Verbo | Operação | Uso |
|-------|----------|-----|
| GET | Ler | Recuperar dados (lista ou por ID) |
| POST | Criar | Criar novos recursos |
| PUT | Atualizar (completo) | Substituir um recurso por completo |
| PATCH | Atualizar (parcial) | Atualizar campos específicos |
| DELETE | Deletar | Remover recursos |

## 📝 Notas para Consulta Futura

- Sempre use DTOs ao invés de expor entidades do banco diretamente
- Implemente paginação em endpoints que retornam listas
- Use o verbo HTTP correto para cada operação
- Valide os dados de entrada nos DTOs
- Configure strings de conexão e outras configurações no appsettings.json
- Utilize injeção de dependência para melhor testabilidade

---

**Data de conclusão:** Novembro 2025

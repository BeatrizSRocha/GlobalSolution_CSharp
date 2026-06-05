using BCrypt.Net;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // Check if database has been seeded
        if (context.CategoriasImpacto.Any())
        {
            return; // DB has been seeded
        }

        // 1. Seed Impact Categories (Strictly the requested ones)
        var categorias = new List<CategoriaImpacto>
        {
            new CategoriaImpacto { Nome = "Saúde", Descricao = "Inovações aplicadas na medicina, tratamento de água e cuidados com a saúde física." },
            new CategoriaImpacto { Nome = "Agricultura", Descricao = "Tecnologias de sensoriamento, monitoramento ambiental e otimização de safras agrícolas." },
            new CategoriaImpacto { Nome = "Consumo", Descricao = "Produtos do dia a dia, desde colchões viscoelásticos a câmeras de smartphones e ferramentas domésticas." }
        };

        context.CategoriasImpacto.AddRange(categorias);
        context.SaveChanges();

        // 2. Seed Default Users (Passwords hashed with BCrypt)
        var usuarios = new List<Usuario>
        {
            new Usuario 
            { 
                Nome = "Administrador do Sistema", 
                Email = "admin@espacial.com", 
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), 
                Perfil = "Administrador" 
            },
            new Usuario 
            { 
                Nome = "Pesquisador Espacial", 
                Email = "pesquisador@espacial.com", 
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("Pesq123!"), 
                Perfil = "Pesquisador" 
            }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();

        // Retrieve saved categories to map IDs correctly
        var catSaude = categorias.First(c => c.Nome == "Saúde").Id;
        var catAgri = categorias.First(c => c.Nome == "Agricultura").Id;
        var catCons = categorias.First(c => c.Nome == "Consumo").Id;

        // 3. Seed Initial Technologies
        var tecnologias = new List<Tecnologia>
        {
            new Tecnologia
            {
                Nome = "Espuma Viscoelástica",
                Descricao = "Espuma originalmente desenvolvida pela NASA Ames Research Center para melhorar a absorção de impactos em assentos de aeronaves e trajes de astronautas.",
                CategoriaImpactoId = catSaude,
                MissaoOrigem = "Programa Aeronáutico da NASA",
                AnoInovacao = 1966,
                BeneficioTerra = "Utilizada globalmente em colchões ortopédicos, travesseiros e assentos automotivos de alta performance."
            },
            new Tecnologia
            {
                Nome = "Sensores de Imagem CMOS",
                Descricao = "Câmeras minúsculas em pastilhas de silício desenvolvidas pelo Jet Propulsion Laboratory (JPL) para equipar espaçonaves com câmeras digitais de alta resolução.",
                CategoriaImpactoId = catCons,
                MissaoOrigem = "Exploração Planetária Interestelar",
                AnoInovacao = 1993,
                BeneficioTerra = "Tecnologia fundamental que permite as câmeras em todos os smartphones modernos, webcams e endoscopia por imagem médica."
            },
            new Tecnologia
            {
                Nome = "Filtros de Purificação de Água",
                Descricao = "Sistemas avançados baseados em íons de prata e carvão ativado criados para reciclar e purificar o suprimento de água dos astronautas a bordo das naves Apollo.",
                CategoriaImpactoId = catSaude,
                MissaoOrigem = "Missões Apolo à Lua",
                AnoInovacao = 1970,
                BeneficioTerra = "Purificadores de água domésticos e tratamento de recursos hídricos em comunidades sem acesso a saneamento básico."
            },
            new Tecnologia
            {
                Nome = "Lentes Antirrisco",
                Descricao = "Revestimento protetor de carbono desenvolvido para proteger viseiras de astronautas e painéis de naves contra impactos e detritos orbitais.",
                CategoriaImpactoId = catCons,
                MissaoOrigem = "Missões Skylab e Ônibus Espacial",
                AnoInovacao = 1980,
                BeneficioTerra = "Revestimento aplicado na maioria das lentes de óculos modernas, aumentando a durabilidade de viseiras esportivas e óculos de grau."
            },
            new Tecnologia
            {
                Nome = "Ferramentas Sem Fio de Alta Potência",
                Descricao = "Motores eletromagnéticos leves de baixo consumo desenvolvidos em parceria com a Black & Decker para perfuração e coleta de amostras de rochas na superfície lunar.",
                CategoriaImpactoId = catCons,
                MissaoOrigem = "Missões Lunar Apollo 15 e 16",
                AnoInovacao = 1971,
                BeneficioTerra = "Precursora dos aspiradores de pó portáteis sem fio (Dustbuster), furadeiras domésticas e ferramentas cirúrgicas sem cabo."
            },
            new Tecnologia
            {
                Nome = "Sensoriamento Remoto para Culturas",
                Descricao = "Análise de imagens termais e multiespectrais por satélites para medir índices de vigor de vegetação e umidade do solo.",
                CategoriaImpactoId = catAgri,
                MissaoOrigem = "Programa Satélite Landsat",
                AnoInovacao = 1972,
                BeneficioTerra = "Permite o agronegócio de precisão com aplicação localizada de defensivos, irrigação inteligente e monitoramento de pragas em larga escala."
            }
        };

        context.Tecnologias.AddRange(tecnologias);
        context.SaveChanges();
    }
}

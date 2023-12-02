using System;

// Classe base para Canal
public abstract class Canal
{
    protected string Destinatario { get; }

    protected Canal(string destinatario)
    {
        Destinatario = destinatario;
    }

    public abstract void EnviarMensagem(Mensagem mensagem);
}

// Classes derivadas para Canais específicos
public class CanalWhatsApp : Canal
{
    public CanalWhatsApp(string numeroTelefone) : base(numeroTelefone) { }

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem para {Destinatario} pelo WhatsApp: {mensagem.Conteudo()} ({mensagem.DataEnvio})");
    }
}

public class CanalTelegram : Canal
{
    public CanalTelegram(string usuario) : base(usuario) { }

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem para @{Destinatario} pelo Telegram: {mensagem.Conteudo()} ({mensagem.DataEnvio})");
    }
}

public class CanalFacebook : Canal
{
    public CanalFacebook(string usuario) : base(usuario) { }

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem para {Destinatario} pelo Facebook: {mensagem.Conteudo()} ({mensagem.DataEnvio})");
    }
}

public class CanalInstagram : Canal
{
    public CanalInstagram(string usuario) : base(usuario) { }

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem para {Destinatario} pelo Instagram: {mensagem.Conteudo()} ({mensagem.DataEnvio})");
    }
}

public class CanalEmail : Canal
{
    public CanalEmail(string enderecoEmail) : base(enderecoEmail) { }

    public override void EnviarMensagem(Mensagem mensagem)
    {
        Console.WriteLine($"Enviando mensagem para {Destinatario} por e-mail: {mensagem.Conteudo()} ({mensagem.DataEnvio})");
    }
}

// Classe base para Mensagem
public abstract class Mensagem
{
    public string Texto { get; set; }
    public DateTime DataEnvio { get; set; }

    public abstract string Conteudo();
}

// Classes derivadas para diferentes tipos de Mensagens
public class MensagemTexto : Mensagem
{
    public override string Conteudo()
    {
        return Texto;
    }
}

public class MensagemVideo : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }
    public int Duracao { get; set; }

    public override string Conteudo()
    {
        return $"Vídeo: {Arquivo} ({Formato}), Duração: {Duracao} segundos";
    }
}

public class MensagemFoto : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public override string Conteudo()
    {
        return $"Foto: {Arquivo} ({Formato})";
    }
}

public class MensagemArquivo : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public override string Conteudo()
    {
        return $"Arquivo: {Arquivo} ({Formato})";
    }
}

// Enum para representar as opções de rede social
public enum RedeSocialEnum
{
    WhatsApp,
    Telegram,
    Facebook,
    Instagram,
    Email
}

// Classe principal
public class Programa
{
    public static void Main()
    {
        Console.WriteLine("Escolha a rede social:");
        Console.WriteLine("1. WhatsApp");
        Console.WriteLine("2. Telegram");
        Console.WriteLine("3. Facebook");
        Console.WriteLine("4. Instagram");
        Console.WriteLine("5. Email");

        int escolhaRedeSocial;
        if (int.TryParse(Console.ReadLine(), out escolhaRedeSocial) && Enum.IsDefined(typeof(RedeSocialEnum), escolhaRedeSocial - 1))
        {
            Canal canal = CriarCanal((RedeSocialEnum)(escolhaRedeSocial - 1));
            InteragirComUsuario(canal);
        }
        else
        {
            Console.WriteLine("Escolha de rede social inválida.");
        }
    }

    private static Canal CriarCanal(RedeSocialEnum escolhaRedeSocial)
    {
        switch (escolhaRedeSocial)
        {
            case RedeSocialEnum.WhatsApp:
                Console.Write("Digite o número de telefone: ");
                string numeroTelefone = Console.ReadLine();
                return new CanalWhatsApp(numeroTelefone);
            case RedeSocialEnum.Telegram:
                Console.Write("Digite o nome de usuário: ");
                string usuario = Console.ReadLine();
                return new CanalTelegram(usuario);
            case RedeSocialEnum.Facebook:
                Console.Write("Digite o nome de usuário no Facebook: ");
                string usuarioFacebook = Console.ReadLine();
                return new CanalFacebook(usuarioFacebook);
            case RedeSocialEnum.Instagram:
                Console.Write("Digite o nome de usuário no Instagram: ");
                string usuarioInstagram = Console.ReadLine();
                return new CanalInstagram(usuarioInstagram);
            case RedeSocialEnum.Email:
                Console.Write("Digite o endereço de e-mail: ");
                string enderecoEmail = Console.ReadLine();
                return new CanalEmail(enderecoEmail);
            default:
                throw new ArgumentException("Escolha de rede social inválida.");
        }
    }

    private static void InteragirComUsuario(Canal canal)
    {
        Console.WriteLine("Escolha o tipo de mensagem:");
        Console.WriteLine("1. Texto");
        Console.WriteLine("2. Video");
        Console.WriteLine("3. Foto");
        Console.WriteLine("4. Arquivo");

        int escolhaTipoMensagem;
        if (int.TryParse(Console.ReadLine(), out escolhaTipoMensagem) && Enum.IsDefined(typeof(TipoMensagem), escolhaTipoMensagem - 1))
        {
            TipoMensagem tipoMensagem = (TipoMensagem)(escolhaTipoMensagem - 1);
            Mensagem mensagem = CriarMensagem(tipoMensagem);
            canal.EnviarMensagem(mensagem);
        }
        else
        {
            Console.WriteLine("Escolha de tipo de mensagem inválida.");
        }
    }

    private static Mensagem CriarMensagem(TipoMensagem tipoMensagem)
    {
        Mensagem mensagem;
        switch (tipoMensagem)
        {
            case TipoMensagem.Texto:
                mensagem = new MensagemTexto();
                break;
            case TipoMensagem.Video:
                mensagem = new MensagemVideo();
                break;
            case TipoMensagem.Foto:
                mensagem = new MensagemFoto();
                break;
            case TipoMensagem.Arquivo:
                mensagem = new MensagemArquivo();
                break;
            default:
                throw new ArgumentException("Tipo de mensagem inválido.");
        }

        PreencherDadosMensagem(mensagem);
        return mensagem;
    }

   private static void PreencherDadosMensagem(Mensagem mensagem)
{
    Console.Write("Digite a mensagem: ");
    mensagem.Texto = Console.ReadLine();
    mensagem.DataEnvio = DateTime.Now;

    if (mensagem is MensagemVideo)
    {
        MensagemVideo mensagemVideo = mensagem as MensagemVideo;
        Console.Write("Digite o arquivo do vídeo: ");
        mensagemVideo.Arquivo = Console.ReadLine();
        Console.Write("Digite o formato do vídeo: ");
        mensagemVideo.Formato = Console.ReadLine();
        Console.Write("Digite a duração do vídeo (minutos:segundos): ");
        string duracaoInput = Console.ReadLine();

        // Convertendo o formato minutos:segundos para segundos
        if (TryParseDuracao(duracaoInput, out int duracao))
        {
            mensagemVideo.Duracao = duracao;
        }
        else
        {
            Console.WriteLine("Formato de duração inválido. Usando duração padrão de 0 segundos.");
        }
    }
    else if (mensagem is MensagemFoto)
    {
        MensagemFoto mensagemFoto = mensagem as MensagemFoto;
        Console.Write("Digite o arquivo da foto: ");
        mensagemFoto.Arquivo = Console.ReadLine();
        Console.Write("Digite o formato da foto: ");
        mensagemFoto.Formato = Console.ReadLine();
    }
    else if (mensagem is MensagemArquivo)
    {
        MensagemArquivo mensagemArquivo = mensagem as MensagemArquivo;
        Console.Write("Digite o arquivo: ");
        mensagemArquivo.Arquivo = Console.ReadLine();
        Console.Write("Digite o formato do arquivo: ");
        mensagemArquivo.Formato = Console.ReadLine();
    }
}

private static bool TryParseDuracao(string duracaoInput, out int duracao)
{
    duracao = 0;

    string[] partes = duracaoInput.Split(':');

    if (partes.Length == 1)
    {
        // Se houver apenas uma parte, tenta converter para int e considera como segundos
        if (int.TryParse(partes[0], out int segundos))
        {
            duracao = segundos;
            return true;
        }
    }
    else if (partes.Length == 2)
    {
        // Se houver duas partes, tenta converter minutos e segundos para int e calcula a duração total em segundos
        if (int.TryParse(partes[0], out int minutos) && int.TryParse(partes[1], out int segundos))
        {
            duracao = minutos * 60 + segundos;
            return true;
        }
    }

    return false;
}


}

// Enum para representar as opções de tipo de mensagem
public enum TipoMensagem
{
    Texto,
    Video,
    Foto,
    Arquivo
}

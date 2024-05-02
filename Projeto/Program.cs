using Projeto;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;


namespace Projeto
{
    class Program
    {
        static List<CCorrente> contas = new List<CCorrente>();

        static void Main(string[] args)
        {
            string? i;
            int op;

            int Menu()
            {

                Console.WriteLine("--- Menu ---");
                Console.WriteLine("1. Acesso Administrativo");
                Console.WriteLine("2. Caixa Eletrônico");
                Console.WriteLine("3. Sair");
                Console.Write("Digite sua opção: ");

                i = Console.ReadLine();
                Int32.TryParse(i, out op);
                Console.WriteLine();

                return op;
            }

            do
            {
                op = Menu();
                switch (op)
                {
                    case 1:
                        AcessoAdm();
                        break;
                    case 2:
                        CaixaEletro();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("---> Opção inválida, redigite por favor. \n");
                        break;
                }

            } while (op != 3);

            static void AcessoAdm()
            {
                bool voltar = false;
                while (!voltar)
                {
                    Console.WriteLine("--- Menu do acesso administrativo ---");
                    Console.WriteLine("1. Cadastrar uma Conta Corrente");
                    Console.WriteLine("2. Mostrar os saldos de todas as contas");
                    Console.WriteLine("3. Excluir uma conta");
                    Console.WriteLine("4. Voltar");
                    Console.Write("Digite sua opção: ");

                    int op = 0;
                    string? i;
                    i = Console.ReadLine();
                    Int32.TryParse(i, out op);
                    Console.WriteLine();

                    switch (op)
                    {
                        case 1:
                            CadastroCC();
                            break;
                        case 2:
                            MostrarSaldos();
                            break;
                        case 3:
                            ExcluirCC();
                            break;
                        case 4:
                            voltar = true;
                            break;
                        default:
                            Console.WriteLine("--> Opção inválida, redigite por favor. \n");
                            break;
                    }
                }
            }

            static void CadastroCC()
            {
                string? i;
                double lim;

                Console.Write("Digite o número da conta:");
                string num = Console.ReadLine(); // é uma string para adicionar na lista

                Console.Write("Digite o limite da conta:");
                i = Console.ReadLine();
                Double.TryParse(i, out lim);

                contas.Add(new CCorrente(num, lim)); // cria uma nova cc com um num e um lim e a add à lista de contas existentes
                Console.WriteLine("---> Conta cadastrada! =)\n");
            }

            static void MostrarSaldos()
            {
                foreach (var conta in contas) //percorre cada elemento da lista 'contas'
                {
                    if (conta.status == false)
                        continue; // nao mostra as contas excluidas
                    Console.WriteLine($"Conta: {conta.numero}, Saldo: {conta.saldo}"); // o $ substitui pelos valores reais do num e do saldo da conta
                }
            }

            static void ExcluirCC()
            {
                Console.Write("Digite o número da conta que deseja excluir:");
                string num = Console.ReadLine();

                CCorrente conta = contas.FirstOrDefault(c => c.numero == num); // busca na lista 'contas' p encontrar a primeira instância de CCorrente, em q o num da conta seja igual a var num
                conta.status = false; // 'exlui' a conta
                Console.WriteLine("---> Conta excluída! \n");

            }

            static void CaixaEletro()
            {
                Console.Write("Digite o número da conta em que deseja realizar uma operação: ");
                string num = Console.ReadLine();
                CCorrente conta = contas.FirstOrDefault(c => c.numero == num);

                while (conta == null || conta.status == false) //verifica se a var conta é null  ( nao exite conta com o num digitado) ou se foi excluida
                {
                    Console.WriteLine("---> Desculpe, não é possível encontrar essa conta.");
                    Console.Write("Redigite, com uma conta válida, por favor: ");
                    num = Console.ReadLine();
                    conta = contas.FirstOrDefault(c => c.numero == num);
                }

                bool voltar = false;
                while (!voltar)
                {
                    Console.WriteLine("--- Menu do caixa eletrônico ---");
                    Console.WriteLine("1. Saque");
                    Console.WriteLine("2. Deposito");
                    Console.WriteLine("3. Transferencia");
                    Console.WriteLine("4. Voltar");

                    int op;
                    double valor;
                    string? i;
                    i = Console.ReadLine();
                    Int32.TryParse(i, out op);

                    switch (op)
                    {
                        case 1:
                            Console.WriteLine("Digite o valor do saque:");
                            i = Console.ReadLine();
                            Double.TryParse(i, out valor);

                            if (conta.Sacar(valor))
                            {
                                Console.WriteLine("---> Saque realizado!");
                            }
                            else
                                Console.WriteLine("---> Saldo inválido para esse saque.");

                            break;

                        case 2:
                            Console.WriteLine("Digite o valor do depósito:");
                            i = Console.ReadLine();
                            Double.TryParse(i, out valor);

                            if (conta.Depositar(valor))
                                Console.WriteLine("---> Depósito realizado!");
                            else
                                Console.WriteLine("---> Valor de deposito inválido.");
                            break;

                        case 3:
                            Console.WriteLine("Digite o número da conta para realizar a transferência:");
                            string n_trans = Console.ReadLine();
                            CCorrente cc_trans = contas.FirstOrDefault(c => c.numero == n_trans);

                            while (cc_trans == null || conta.status == false)
                            {
                                Console.WriteLine("---> Desculpe, não é possível encontrar essa conta.");
                                Console.Write("Redigite, com uma conta válida, por favor: ");
                                n_trans = Console.ReadLine();
                                cc_trans = contas.FirstOrDefault(c => c.numero == n_trans);
                            }

                            Console.WriteLine("Digite o valor da transferência:");
                            i = Console.ReadLine();
                            Double.TryParse(i, out valor);
                            if (conta.Transferir(cc_trans, valor))
                                Console.WriteLine("--> Transferência realizada!");
                            else
                                  Console.WriteLine("---> Valor de deposito inválido.");
                            break;

                        case 4:
                            voltar = true;
                            break;
                        default:
                            Console.WriteLine("---> Opção inválida, redigite por favor. \n");
                            break;
                    }
                }
            }
        }
    }
}
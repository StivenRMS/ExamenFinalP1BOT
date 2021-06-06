using PizzaBot.emojis;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace PizzaBot.Clases
{
    class clsEjemplo1
    {
        private static TelegramBotClient Bot;
        public async Task InicioEjemploTelegram()
        {

            

            Bot = new TelegramBotClient("1806528155:AAEvc8Yxz2aVwK4vbO0qVrqqKTppGjPrwpE");


            var me = await Bot.GetMeAsync();
            Console.Title = me.Username;

            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");

            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text)
                return;

            switch (message.Text.Split(' ').First())
            {
                // Send inline keyboard
                case "/Paso_1":
                    await SendInlineKeyboard(message);
                    break;

                // send  keyboard
                case "/Paso_2":
                    await SendInlineKeyboard1(message);
                    break;

                // request location or contact
                case "/Paso_3":
                    await RequestContactAndLocation(message);
                    break;

                // send a photo
                case "/Paso_5":
                    await SendDocument(message);
                    break;

                case "/Paso_4":
                    await SendInlineKeyboardp5(message);
                    break;

                case "/Factura":
                    await Facture(message);
                   
                    break;

                case "1111111":
                    await Factur(message);
                    break;

                case "Juan":
                    await Factu(message);
                    break;

                default:
                    await Usage(message);
                    break;
            }

            // Send inline keyboard
            // You can process responses in BotOnCallbackQueryReceived handler
            static async Task SendInlineKeyboard(Message message)
            {
                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Grande ","Pizza Grande"),
                        InlineKeyboardButton.WithCallbackData("Mediana", "Pizza Mediana"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Pequeña", "Pizza Pequeña"),
                        InlineKeyboardButton.WithCallbackData("Mini", "Mini Pizza"),
                    }
                });
                await Bot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Elige el tamaño de tu Pizza:{mdEmonji.mano}{mdEmonji.carapensante}",
                    replyMarkup: inlineKeyboard
                    
                ); 
            }

            

            static async Task SendInlineKeyboard1(Message message)
            {
                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Pepperoni","1 Pepperoni"),
                        InlineKeyboardButton.WithCallbackData("5 Carnes","1. 5 Carnes"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Hawaiana", "1 Hawaiana"),
                        InlineKeyboardButton.WithCallbackData("Jamón", " 1 Jamón"),
                    }
                });
                await Bot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Elige de que la quiere:{mdEmonji.caralentesoscuros} ",
                    replyMarkup: inlineKeyboard
                );
            }

            static async Task SendDocument(Message message)
            {
                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

                const string filePath1 = @"C:\Users\ramos\Desktop\1.1\PizzaBot\PizzaBot\330-349.jpg";
                using var fileStream = new FileStream(filePath1, FileMode.Open, FileAccess.Read, FileShare.Read);
                var fileName = filePath1.Split(Path.DirectorySeparatorChar).Last();
                await Bot.SendPhotoAsync(
 
                    chatId: message.Chat.Id,
                    photo: new InputOnlineFile(fileStream, fileName),
                    caption: "Tú orden está lista"

                );
            }

            static async Task RequestContactAndLocation(Message message)
            {
               
                var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    KeyboardButton.WithRequestLocation("Location"),
                    KeyboardButton.WithRequestContact("Contact"),
                });
                await Bot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Quíen o dónde estás tu?",
                    replyMarkup: RequestReplyKeyboard
                );
            }


            static async Task Facture(Message message)
            {
               
                
                

                const string usage = "Ingrese datos de su factura\n" +
                                       "Nit:  ";
                
                

                await Bot.SendTextMessageAsync(
                   chatId: message.Chat.Id,
                    text: $" {usage}",
                    replyMarkup: new ReplyKeyboardRemove()
                    
                );

            }

            static async Task Factur(Message message)
            {

                
                

                string usage = "Ingrese datos de su factura\n" +
                                       "Nombre:  ";
                
                


                await Bot.SendTextMessageAsync(
                   chatId: message.Chat.Id,
                    text: $" {usage}",
                    replyMarkup: new ReplyKeyboardRemove()

                );

            }

            static async Task Factu(Message message)
            {

                

                string usage = "Ingrese datos de su factura\n" +
                                       "Cantidad Gastada:  ";



                await Bot.SendTextMessageAsync(
                   chatId: message.Chat.Id,
                    text: $" {usage}",
                    replyMarkup: new ReplyKeyboardRemove()

                );

            }



            static async Task Usage(Message message)
            {
                string nombre_manda_mensaje = message.Chat.FirstName.ToString();
                string apellidos = message.Chat.LastName.ToString();
                string Nom = $"HOLA {nombre_manda_mensaje} {apellidos}, ";
                const string usage ="MENU para Orden de Pizzas :\n*Sigue los pasos...*\n" +
                                    "/Paso_1        \n" +
                                    "/Paso_2        \n" +
                                    "/Paso_3        \n" +
                                    "/Paso_4        \n" +
                                    "/Paso_5        \n" +
                                    "/Factura   ";
                await Bot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Nom + usage,
                    replyMarkup: new ReplyKeyboardRemove()
                );
            }
        }

        private static async Task SendInlineKeyboardp5(Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            // Simulate longer running task
            await Task.Delay(500);

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Pago en Efecivo","Efectivo"),
                        InlineKeyboardButton.WithCallbackData("Pago por Tarjeta","Tarjeta"),
                    },
                    
                });
            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Tipo de pago:{mdEmonji.caralentesoscuros} ",
                replyMarkup: inlineKeyboard
            );



        }

        // Process Inline Keyboard callback data
        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            await Bot.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Recibido!, {callbackQuery.Data}"

            );

            await Bot.SendTextMessageAsync(
                chatId: callbackQuery.Message.Chat.Id,
                text: $"Recibido!, {callbackQuery.Data}"
            );

           
        }

       


        #region Inline Mode

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

            InlineQueryResultBase[] results = {
                // displayed result
                new InlineQueryResultArticle(
                    id: "3",
                    title: "TgBots",
                    inputMessageContent: new InputTextMessageContent(
                        "hello"
                    )
                )
            };
            await Bot.AnswerInlineQueryAsync(
                inlineQueryId: inlineQueryEventArgs.InlineQuery.Id,
                results: results,
                isPersonal: true,
                cacheTime: 0
            );
        }

        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        #endregion

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message
            );
        }




    }//fin de la clase
}

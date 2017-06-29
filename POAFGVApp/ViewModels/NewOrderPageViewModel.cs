using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace POAFGVApp.ViewModels
{
    public class NewOrderPageViewModel : BaseViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }

        public NewOrderPageViewModel()
        {
            var messages = new List<Message>()
            {
                new Message()
                {
                    Text = "Olá, bem vindo ao PizzaBot!",
                     IsIncoming = true,
                    MessageDateTime = DateTime.Now.AddMinutes(2)
                },
                new Message()
                {
                    Text = "Olá, gostaria de pedir uma pizza",
                     IsIncoming = false,
                    MessageDateTime = DateTime.Now.AddMinutes(4)
                },
                new Message()
                {
                    Text = "Claro, qual o sabor?",
                     IsIncoming = true,
                    MessageDateTime = DateTime.Now.AddMinutes(8)
                },
                new Message()
                {
                    Text = "Quero uma de calabresa e uma de portuguesa",
                     IsIncoming = false,
                    MessageDateTime = DateTime.Now.AddMinutes(10)
                },
                new Message()
                {
                    Text = "Perfeito, seu pedido está concluído",
                     IsIncoming = true,
                    MessageDateTime = DateTime.Now.AddMinutes(12)
                }
            };

            Messages = new ObservableCollection<Message>(messages);
        }
    }
}

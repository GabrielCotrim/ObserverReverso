using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppObserverExemple
{
    interface IObserver
    {
        void Update(ISubject subject);
    }

    interface ISubject
    {
        int Count { get; set; }
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }

    class Subject : ISubject
    {

        public int Count { get; set; } = 0;

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    class Subject2 : ISubject
    {
        public int Count { get; set; } = 0;

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    class BadgeObserver : IObserver
    {

        public static BadgeObserver Intance = new BadgeObserver();
        private BadgeObserver() { }

        public int Count { get; private set; } = 0;

        public void Update(ISubject subject)
        {
            Count += subject.Count;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var subject1 = new Subject
            {
                Count = 5
            };

            var subject2 = new Subject2
            {
                Count = 3
            };

            subject1.Attach(BadgeObserver.Intance);
            subject2.Attach(BadgeObserver.Intance);

            subject1.Notify();

            Console.WriteLine($"Observador recebe valor do sujeito 1: {BadgeObserver.Intance.Count}");

            subject2.Notify();

            Console.WriteLine($"Observador recebe valor do sujeito 2, consequentemente somando ao valor do sujeito 1: {BadgeObserver.Intance.Count}");

            subject1.Count = 23;

            Console.WriteLine($"Sujeito 1 atualiza o valor da propriedade count porém não notifica o observador: {BadgeObserver.Intance.Count}");

            subject1.Notify();

            Console.WriteLine(BadgeObserver.Intance.Count);

            Console.ReadKey();
        }
    }
}

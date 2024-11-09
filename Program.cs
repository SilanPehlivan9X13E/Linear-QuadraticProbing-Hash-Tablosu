using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ödev10
{
    /*Bu  projede rastgele üretilen anahtarları hem linear probing hem de quadratic probing yöntemleriyle hash tablosuna yerleştirilme işlemi yapıcaz.
     * Bu iki yöntem arasındaki fark, çakışma çözme stratejilerindedir.
     * Linear probing, çakışma durumunda sırayla bir sonraki boş alana bakar, quadratic probing ise adımları kareli olarak artırarak yer arar.*/
/*Hash tablosu, anahtar-değer çiftlerini depolamak için kullanılan bir veri yapısıdır. 
 * Anahtarları bir hash fonksiyonu ile belirli bir dizine (index) dönüştürür ve bu dizin üzerinde işlemler yapılır.
 * Bu projede, hash fonksiyonu olarak division method (bölme yöntemi) kullanılmaktadır.*/



    class HashTable
    {
        private const int Size = 100; // Hash tablosunun boyutunu belittim. Const yapısı tablonun boyutunu sabit tutmamızı sağlar .Sonradan değişmemizi önler!!
        private int[] table; // Hash tablosunu tutması için bir dizi yazarız 
        private Random random; //Rastgele sayı üretmek için

        public HashTable() //HashTable sınıfından bir yapıcı metot oluştururuz.
        {
            table = new int[Size]; //Tabloda 100 boyutlu bir dizi oluştururuz
            random = new Random(); //Rastgele sayı üretmeye başlıyooruz..
            for (int i = 0; i < Size; i++) //Bu döngü ile hash tablosunun her bir elemanına -1 değeri atanır.
            {                               //-1, tablonun o indeksinin boş olduğunu gösterir.
            
                table[i] = -1; // -1, dizi boş
            }
        }

        // Hash fonksiyonu: Division yöntemi
        private int Hash(int anahtar)
        {
            return anahtar % Size; //Örneğin: Anahtar değeri 345 ise, 345 % 100 = 45 olur, yani hash tablosunun 45. indeksine yerleştirilir.
        }

        // Linear Probing ile ekleme
        public void LinearProbingEkleme(int anahtar)
        {
            int index = Hash(anahtar);
            int baslangıctakiIndex = index;
            int i = 0; //sayacı sıfırdan başlartırız ve her çakışma durumunda sayaç bir bir artar.

            // Çakışma durumu olursa linear probing ile yeni bir indeks arıyoruz
            while (table[index] != -1)
            {
                i++;
                index = (baslangıctakiIndex + i) % Size; // Yeni index bulmak için
                //Yeni indeks hesaplanır.
                //baslangıctakiIndex'ten başlayarak, i kadar adım ilerlenir ve sonra tablonun boyutuyla (Size) modül alınır. Bu işlem, tablonun dışına çıkmamak için yapıyoruzz
                if (i >= Size) return; // Tüm tablo dolmuşsa işlem sonlanırdımasını yaparız
            }
            table[index] = anahtar;
        }

        // Quadratic Probing ile ekleme
        public void QuadraticProbingEkleme(int anahtar)
        {
            int index = Hash(anahtar);
            int originalIndex = index;
            int i = 0;

            // Çakışma durumunda quadratic probing ile yeni bir indeks arıyoruz
            while (table[index] != -1)
            {
                i++;
                index = (originalIndex + i * i) % Size; // Kareli artış ile yeni index bulma
                if (i >= Size) return; // Tüm tablo dolmuşsa işlem sonlanır
            }
            table[index] = anahtar; //tabloda boş yer varsa anahtar bu indekse eklemesini yaparız.
        }

        // Hash tablosunu yazdırmak için olluşturduğum metot
        public void TabloYazdırma()
        {
            Console.WriteLine("Hash Tablosu:");
            for (int i = 0; i < Size; i++) //Tablodaki her elemanı yazdırmak için bir for döngüsü oluştururuzzz
            {
                Console.WriteLine($"Index {i}: {table[i]}");
            }
        }

        // Rastgele anahtar üretme
        public int RastgeleAnahtarUretmekIcın()
        {
            return random.Next(1, 200); // 1 ile 200 arasında rastgele anahtar üretmesini random ile isterim
        }
    }


        internal class Program
    {
        static void Main(string[] args)
        {
            HashTable hashTableLinear = new HashTable();  // Linear Probing tablosu
            HashTable hashTableQuadratic = new HashTable();  // Quadratic Probing tablosu

            // 100 rastgele anahtar üretme ve her iki yönteme ekleme
            for (int i = 0; i < 100; i++)
            {
                int key = hashTableLinear.RastgeleAnahtarUretmekIcın();
                hashTableLinear.LinearProbingEkleme(key);  // Linear probing ile ekleme
                hashTableQuadratic.QuadraticProbingEkleme(key);  // Quadratic probing ile ekleme
            }

            // Linear Probing hash tablosunu yazdırma işlemi yaparım
            Console.WriteLine("Linear Probing ile Hash Tablosu:");
            hashTableLinear.TabloYazdırma();

            // Quadratic Probing hash tablosunu yazdırma işlemi yaparımm
            Console.WriteLine("\nQuadratic Probing ile Hash Tablosu:");
            hashTableQuadratic.TabloYazdırma();

            Console.ReadLine();
        }
    }
}

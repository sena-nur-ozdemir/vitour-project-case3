# 🌍 Vitour – Yapay Zeka Destekli Tur Rezervasyon ve Yönetim Sistemi | Bootcamp Case #3

<br>

Bu proje, **M&Y Yazılım Eğitim Akademi .NET Full Stack Bootcamp** kapsamında, **Murat Yücedağ** rehberliğinde geliştirilen **Case #3** çalışmasıdır. 

Vitour, modern bir seyahat acentesinin tüm operasyonlarını yönetebilmesi için geliştirilmiş, **yapay zeka destekli** uçtan uca bir web uygulamasıdır. Projede **ASP.NET Core MVC** ve **MongoDB** altyapısı kullanılmış; özellikle admin panelinin tasarımı ve harita görselleri tamamen AI (Claude & Gemini) ile üretilerek sisteme entegre edilmiştir.

---
## 🎯 Projenin Amacı

Bu proje ile aşağıdaki konularda derinlemesine pratik yapılmış ve yetkinlik kazanılmıştır:
* **NoSQL** mantığını kavrama ve **MongoDB** ile CRUD işlemleri gerçekleştirme.
* MVC mimarisinde **View Component** ve **Razor Pages** yeteneklerini maksimum seviyede kullanarak modüler arayüzler geliştirme.
* Yazılım geliştirme süreçlerinde **Yapay Zeka (AI)** araçlarını aktif kullanarak UI/UX tasarımını ve kodlama hızını optimize etme.
* DTO ve AutoMapper kullanarak veri transfer süreçlerini yönetme.
* Üçüncü parti raporlama kütüphaneleri (iText, ClosedXML) ile dinamik belgeler oluşturma.

---

## 🚀 Temel Özellikler

### 👤 Kullanıcı Arayüzü (Vitrin)
* **Modüler Yapı (View Components):** Sayfa içindeki bağımsız parçaların (footer, navbar, listeler vb.) View Component mimarisi ile dinamik ve modüler şekilde yönetilmesi.
* **Dinamik Listeleme ve Filtreleme:** MongoDB'den çekilen verilerle anlık kategori, tur tipi ve süre filtrelemesi. Dinamik sayfalama (Pagination) altyapısı.
* **Gelişmiş Tur Detay Sayfası (5 Sekmeli Yapı):**
  * **Açıklama & Program:** Gün gün detaylandırılmış tur akışları.
  * **AI Rota Haritaları:** Google Maps yerine, **Gemini AI** kullanılarak projeye özel üretilmiş konsept rota görselleri.
  * **Dinamik Yorum ve Puanlama:** Kullanıcıların 4 farklı kritere göre verdiği puanların matematiksel ortalamasının hesaplanması.
* **Akıllı Rezervasyon Akışı:** Kapasite kontrolü yapan, kontenjan dolduğunda otomatik engelleme sağlayan güvenli rezervasyon sistemi.
* **Çoklu Dil Desteği:** Cookie tabanlı Localization altyapısı ile Türkçe - İngilizce dil seçeneği.

### 🛡️ Admin Paneli (Yönetim)
* **Yapay Zeka Tasarımlı Arayüz:** Sıfırdan **Claude AI** kullanılarak prompt engineering ile tasarlanmış, modern ve kullanıcı dostu yönetim paneli.
* **Canlı Önizlemeli CRUD İşlemleri:** Tur eklerken veya düzenlerken, içeriğin vitrinde nasıl görüneceğini anlık olarak gösteren interaktif Razor View tasarımı.
* **Dinamik Tur Rozetleri:** Turlara "Popüler, Fırsat, Yeni" gibi etiketlerin admin panelinden atanabilmesi.
* **Dashboard ve KPI Metrikleri:** Aylık hedefler, toplam aktif tur sayısı ve hasılat verilerinin istatistiksel sunumu.

### 📊 Raporlama Sistemi
* **Gelişmiş PDF Çıktıları:** **iText 9** kütüphanesi ve özel font entegrasyonu ile **Türkçe karakter sorunu tamamen çözülmüş** dinamik PDF raporları.
* **Excel Dışa Aktarım:** **ClosedXML** ile tüm koleksiyonlar (Turlar, Yorumlar, Rezervasyonlar) için detaylı Excel çıktıları.

---

## 🛠 Kullanılan Teknolojiler

**Backend & Temel Yapı**
* ASP.NET Core MVC 8.0
* Razor Views & View Components
* Asenkron Programlama (async/await)

**Veritabanı**
* MongoDB (NoSQL)
* MongoDB.Driver

**Yapay Zeka & Tasarım (AI-Driven)**
* **Claude AI:** Admin paneli UI/UX tasarımı ve rezervasyon arayüzü kodlaması.
* **Gemini AI:** Tur rotaları için özelleştirilmiş harita görselleri üretimi.

**Araçlar ve Kütüphaneler**
* **AutoMapper:** DTO (Data Transfer Object) dönüşümleri için.
* **iText 9:** Dinamik PDF raporlama için.
* **ClosedXML:** Excel veri manipülasyonu için.

---

## 📸 Proje Ekran Görüntüleri

Tüm projenin ekran görüntülerini detaylı incelemek için aşağıdaki başlıklara tıklayabilirsiniz:

<details>
<summary><b>✨ 1. Ana Sayfa Tasarımı, Tur Detayları ve Yapay Zeka Haritaları</b></summary>
<br>

*View Component mimarisiyle oluşturulmuş dinamik vitrin ekranları.*

<img width="3788" height="5413" alt="Vitour_2" src="https://github.com/user-attachments/assets/b195fa94-3958-45d8-b8ce-6ec9f29215ac" />

<img width="3788" height="5252" alt="Vitour_1" src="https://github.com/user-attachments/assets/d92e8cf8-54fb-43cf-b00b-86c9d48e8cdc" />

<img width="3788" height="5657" alt="Vitour_3" src="https://github.com/user-attachments/assets/bbbaebe6-bbd2-41eb-9d10-029c5b631ede" />

<img width="3788" height="5251" alt="Vitour_4" src="https://github.com/user-attachments/assets/479ff3b5-46d7-449b-9426-683999171f53" />

<img width="3788" height="5243" alt="Vitour_5" src="https://github.com/user-attachments/assets/f6f0f619-dace-47dc-a3cf-e816d373a4f8" />

<img width="3788" height="7539" alt="Vitour_6" src="https://github.com/user-attachments/assets/89bc9d43-4730-4105-b402-6c4d37ad1ff0" />

<img width="3788" height="5251" alt="Vitour_7" src="https://github.com/user-attachments/assets/0323f76f-8c7f-4b12-8d99-9e3e3e20dd4a" />

<img width="1897" height="857" alt="Vitour_24" src="https://github.com/user-attachments/assets/38b81b52-a2ef-4088-8748-3db9dfdee353" />

<img width="1897" height="865" alt="Vitour_25" src="https://github.com/user-attachments/assets/32c55be3-6757-4834-9650-6765c6585efb" />

<img width="1897" height="862" alt="Vitour_26" src="https://github.com/user-attachments/assets/f93ff768-4ad1-4a74-899b-a7c0b4de9c87" />

<img width="1897" height="860" alt="Vitour_27" src="https://github.com/user-attachments/assets/ed9de169-ee0a-4cd4-8d05-df90c4eb7377" />

<img width="1896" height="837" alt="vitour_28" src="https://github.com/user-attachments/assets/aee5f9ab-e069-421a-8212-115a057d0ea2" />

<img width="1896" height="861" alt="vitour_29" src="https://github.com/user-attachments/assets/03131c72-4856-462b-9810-7ea57aa222d7" />

<img width="1895" height="862" alt="Vitour_30" src="https://github.com/user-attachments/assets/d7b1e105-e4cd-4378-a76a-fc4507f6ca45" />

<img width="1901" height="855" alt="Vitour_31" src="https://github.com/user-attachments/assets/06c162c2-5352-4ddd-9b8c-fe736feed221" />

</details>

<details>
<summary><b>💳 2. Modern Rezervasyon Akışı</b></summary>
<br>

*Kapasite kontrollü rezervasyon adımları.*

<img width="3788" height="1944" alt="Vitour_8" src="https://github.com/user-attachments/assets/34fb8c25-fbec-4518-a673-e5c0fcc86504" />

<img width="1895" height="862" alt="vitour_2" src="https://github.com/user-attachments/assets/f3aee6af-dab2-4282-98e7-3bd51015ccf3" />

</details>

<details>
<summary><b>🛡️ 3. Yapay Zeka Tasarımlı Admin Paneli</b></summary>
<br>

*Gemini AI ile tasarlanan, KPI kartları ve Canlı Önizlemeli içerik yönetim ekranları.*

<img width="1912" height="862" alt="Vitour_9" src="https://github.com/user-attachments/assets/62bc8e94-767b-4722-aef4-a323f2cb70c1" />

<img width="1917" height="860" alt="Vitour_10" src="https://github.com/user-attachments/assets/4c344ecc-d470-46f8-bb64-3734cc9d5cf8" />

<img width="1917" height="857" alt="Vitour_11" src="https://github.com/user-attachments/assets/2bc862c9-fbce-4383-b7a8-7c7f5b1e004e" />

<img width="1917" height="860" alt="Vitour_12" src="https://github.com/user-attachments/assets/06da2ccf-1f25-4cb2-a33f-e54df67c6e70" />

<img width="1917" height="862" alt="Vitour_13" src="https://github.com/user-attachments/assets/522c0f44-6435-48cf-92c3-540ebef645cc" />

<img width="1917" height="861" alt="Vitour_14" src="https://github.com/user-attachments/assets/457e7b19-a6d6-455e-b969-ed92458a1768" />

<img width="1917" height="867" alt="Vitour_15" src="https://github.com/user-attachments/assets/39cb9558-9733-4572-b151-40ffe1bfa42b" />

<img width="1912" height="862" alt="Vitour_16" src="https://github.com/user-attachments/assets/4b917028-8885-4418-b5f4-6e748816580c" />

<img width="1917" height="862" alt="Vitour_17" src="https://github.com/user-attachments/assets/03dad1e1-3c19-4629-b11d-ab88074171c6" />

<img width="3788" height="2654" alt="Vitour_23" src="https://github.com/user-attachments/assets/34ae5fb0-8dce-4b1b-b0c0-45e78972802d" />

</details>

<details>
<summary><b>📊 4. Excel ve PDF Rapor Çıktıları</b></summary>
<br>

*Sistem üzerinden oluşturulan Türkçe karakter destekli profesyonel rapor çıktıları.*

<img width="982" height="752" alt="Vitour_18_rapor" src="https://github.com/user-attachments/assets/11b15220-42c2-4220-a46c-a5337326e7b1" />

<img width="972" height="706" alt="Vitour_22_rapor" src="https://github.com/user-attachments/assets/d25a7aac-43fd-4be6-9f8a-2958b204e4b5" />

<img width="1607" height="677" alt="Vitour_19_rapor" src="https://github.com/user-attachments/assets/38cfcf25-d72d-491e-a1af-9d3c5e53e3d3" />

<img width="1891" height="782" alt="Vitour_20_rapor" src="https://github.com/user-attachments/assets/eb1b1dbc-e522-4f1a-b30a-956449e068dd" />

<img width="1895" height="641" alt="Vitour_21_rapor" src="https://github.com/user-attachments/assets/0074436f-667d-4b69-9d5b-664282e5da3d" />

</details>

---

## 🎓 Eğitim

Bu proje, **M&Y Yazılım Eğitim Akademi** tarafından verilen **.NET Full Stack Bootcamp** kapsamında geliştirilen Case #3 çalışmasıdır. Bu süreçteki katkılarından dolayı **Murat Yücedağ** hocama teşekkür ederim.

---

## 👩‍💻 Developer

**Sena Nur Özdemir** GitHub: [https://github.com/sena-nur-ozdemir]

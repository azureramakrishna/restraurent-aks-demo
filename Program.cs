var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var menu = new[]
{
    new MenuItem("Butter Chicken", "Tender chicken in creamy tomato sauce", 14.99m, "https://images.unsplash.com/photo-1603894584373-5ac82b2ae398?w=400"),
    new MenuItem("Chicken Biryani", "Aromatic basmati rice with spiced chicken", 13.99m, "https://images.unsplash.com/photo-1563379091339-03b21ab4a4f8?w=400"),
    new MenuItem("Mutton Biryani", "Fragrant rice with tender mutton pieces", 15.99m, "https://images.unsplash.com/photo-1642821373181-696a54913e93?w=400"),
    new MenuItem("Paneer Tikka", "Grilled cottage cheese with spices", 11.99m, "https://images.unsplash.com/photo-1567188040759-fb8a883dc6d8?w=400"),
    new MenuItem("Masala Dosa", "Crispy rice crepe with potato filling", 9.99m, "https://images.unsplash.com/photo-1630383249896-424e482df921?w=400"),
    new MenuItem("Tandoori Chicken", "Clay oven roasted chicken with spices", 15.99m, "https://images.unsplash.com/photo-1610057099443-fde8c4d50f91?w=400"),
    new MenuItem("Samosa", "Crispy pastry with spiced potato filling", 5.99m, "https://images.unsplash.com/photo-1601050690597-df0568f70950?w=400"),
    new MenuItem("Palak Paneer", "Cottage cheese in spinach gravy", 12.99m, "https://images.unsplash.com/photo-1631452180519-c014fe946bc7?w=400"),
    new MenuItem("Chole Bhature", "Spicy chickpeas with fried bread", 10.99m, "https://images.unsplash.com/photo-1626132647523-66f5bf380027?w=400"),
    new MenuItem("Rogan Josh", "Kashmiri lamb curry with aromatic spices", 16.99m, "https://images.unsplash.com/photo-1645177628172-a94c1f96e6db?w=400"),
    new MenuItem("Dal Makhani", "Creamy black lentils slow cooked", 11.99m, "https://images.unsplash.com/photo-1546833999-b9f581a1996d?w=400"),
    new MenuItem("Naan Bread", "Soft leavened flatbread from tandoor", 3.99m, "https://images.unsplash.com/photo-1628840042765-356cda07504e?w=400")
};

var navBar = """
    <nav style='background: rgba(0,0,0,0.3); padding: 15px 0; margin-bottom: 20px;'>
        <div style='max-width: 1200px; margin: 0 auto; display: flex; justify-content: center; gap: 30px;'>
            <a href='/' style='color: white; text-decoration: none; font-size: 18px; font-weight: 500;'>Menu</a>
            <a href='/about' style='color: white; text-decoration: none; font-size: 18px; font-weight: 500;'>About Us</a>
            <a href='/contact' style='color: white; text-decoration: none; font-size: 18px; font-weight: 500;'>Contact</a>
        </div>
    </nav>
""";

var commonStyles = """
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); min-height: 100vh; padding: 20px; }
        .container { max-width: 1200px; margin: 0 auto; }
        header { text-align: center; color: white; padding: 40px 0 20px; }
        h1 { font-size: 48px; margin-bottom: 10px; }
        .tagline { font-size: 18px; opacity: 0.9; }
        .content { background: white; border-radius: 12px; padding: 40px; margin: 20px 0; box-shadow: 0 4px 15px rgba(0,0,0,0.2); }
        .menu-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(280px, 1fr)); gap: 25px; padding: 20px 0; }
        .menu-item { background: white; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.2); transition: transform 0.3s; }
        .menu-item:hover { transform: translateY(-5px); }
        .menu-item img { width: 100%; height: 200px; object-fit: cover; }
        .item-content { padding: 20px; }
        .name { font-size: 22px; font-weight: bold; color: #333; margin-bottom: 8px; }
        .description { color: #666; font-size: 14px; margin-bottom: 15px; line-height: 1.4; }
        .price { color: #ff6b6b; font-size: 24px; font-weight: bold; }
        h2 { color: #333; margin-bottom: 20px; }
        p { color: #666; line-height: 1.8; margin-bottom: 15px; }
        .contact-info { margin: 20px 0; }
        .contact-info p { font-size: 16px; margin: 10px 0; }
    </style>
""";

app.MapGet("/", () => Results.Content($$"""
<!DOCTYPE html>
<html>
<head>
    <title>Maharaja's Palace - Menu</title>
    <meta charset="UTF-8">
    {{commonStyles}}
</head>
<body>
    <div class='container'>
        <header>
            <h1>&#128081; Maharaja's Palace &#128081;</h1>
            <p class='tagline'>Authentic Indian Cuisine</p>
        </header>
        {{navBar}}
        <div class='menu-grid' id='menu'></div>
    </div>
    <script>
        fetch('/api/menu')
            .then(r => r.json())
            .then(items => {
                document.getElementById('menu').innerHTML = items.map(item => `
                    <div class='menu-item'>
                        <img src='${item.image}' alt='${item.name}'>
                        <div class='item-content'>
                            <div class='name'>${item.name}</div>
                            <div class='description'>${item.description}</div>
                            <div class='price'>Rs. ${(item.price * 80).toFixed(0)}</div>
                        </div>
                    </div>
                `).join('');
            });
    </script>
</body>
</html>
""", "text/html"));

app.MapGet("/about", () => Results.Content($$"""
<!DOCTYPE html>
<html>
<head>
    <title>Maharaja's Palace - About Us</title>
    <meta charset="UTF-8">
    {{commonStyles}}
</head>
<body>
    <div class='container'>
        <header>
            <h1>&#128081; Maharaja's Palace &#128081;</h1>
            <p class='tagline'>Authentic Indian Cuisine</p>
        </header>
        {{navBar}}
        <div class='content'>
            <h2>About Us</h2>
            <p>Welcome to Maharaja's Palace, where we bring the authentic flavors of India to your table. Established in 2020, our restaurant has been serving traditional Indian cuisine with a modern touch.</p>
            <p>Our chefs are trained in the art of Indian cooking, using traditional recipes passed down through generations. We source the finest spices and ingredients to ensure every dish is bursting with authentic flavors.</p>
            <p>From the rich curries of North India to the aromatic biryanis of Hyderabad, we offer a diverse menu that celebrates the culinary heritage of India. Each dish is prepared with love and attention to detail.</p>
            <p>We believe in providing not just a meal, but an experience that transports you to the vibrant streets of India. Join us for a memorable dining experience!</p>
        </div>
    </div>
</body>
</html>
""", "text/html"));

app.MapGet("/contact", () => Results.Content($$"""
<!DOCTYPE html>
<html>
<head>
    <title>Maharaja's Palace - Contact Us</title>
    <meta charset="UTF-8">
    {{commonStyles}}
</head>
<body>
    <div class='container'>
        <header>
            <h1>&#128081; Maharaja's Palace &#128081;</h1>
            <p class='tagline'>Authentic Indian Cuisine</p>
        </header>
        {{navBar}}
        <div class='content'>
            <h2>Contact Us</h2>
            <div class='contact-info'>
                <p><strong>&#128205; Address:</strong> Hyderabad, India</p>
                <p><strong>&#128222; Phone:</strong> +91 9513184144</p>
                <p><strong>&#128231; Email:</strong> info@spiceofindia.com</p>
                <p><strong>&#128338; Hours:</strong> 6am - 11pm IST</p>
            </div>
            <p style='margin-top: 30px;'>We'd love to hear from you! Whether you have questions about our menu, want to make a reservation, or just want to share your feedback, feel free to reach out.</p>
        </div>
    </div>
</body>
</html>
""", "text/html"));

app.MapGet("/api/menu", () => menu);

app.Run();

record MenuItem(string Name, string Description, decimal Price, string Image);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var menu = new[]
{
    new MenuItem("Butter Chicken", "Tender chicken in creamy tomato sauce", 14.99m, "https://images.unsplash.com/photo-1603894584373-5ac82b2ae398?w=400"),
    new MenuItem("Biryani", "Aromatic basmati rice with spices and meat", 13.99m, "https://images.unsplash.com/photo-1563379091339-03b21ab4a4f8?w=400"),
    new MenuItem("Paneer Tikka", "Grilled cottage cheese with spices", 11.99m, "https://images.unsplash.com/photo-1567188040759-fb8a883dc6d8?w=400"),
    new MenuItem("Masala Dosa", "Crispy rice crepe with potato filling", 9.99m, "https://images.unsplash.com/photo-1630383249896-424e482df921?w=400"),
    new MenuItem("Tandoori Chicken", "Clay oven roasted chicken with spices", 15.99m, "https://images.unsplash.com/photo-1610057099443-fde8c4d50f91?w=400"),
    new MenuItem("Samosa", "Crispy pastry with spiced potato filling", 5.99m, "https://images.unsplash.com/photo-1601050690597-df0568f70950?w=400"),
    new MenuItem("Palak Paneer", "Cottage cheese in spinach gravy", 12.99m, "https://images.unsplash.com/photo-1631452180519-c014fe946bc7?w=400"),
    new MenuItem("Naan Bread", "Soft leavened flatbread from tandoor", 3.99m, "https://images.unsplash.com/photo-1628840042765-356cda07504e?w=400")
};

app.MapGet("/", () => Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <title>Indian Restaurant</title>
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); min-height: 100vh; padding: 20px; }
        .container { max-width: 1200px; margin: 0 auto; }
        header { text-align: center; color: white; padding: 40px 0; }
        h1 { font-size: 48px; margin-bottom: 10px; }
        .tagline { font-size: 18px; opacity: 0.9; }
        .menu-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(280px, 1fr)); gap: 25px; padding: 20px 0; }
        .menu-item { background: white; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.2); transition: transform 0.3s; }
        .menu-item:hover { transform: translateY(-5px); }
        .menu-item img { width: 100%; height: 200px; object-fit: cover; }
        .item-content { padding: 20px; }
        .name { font-size: 22px; font-weight: bold; color: #333; margin-bottom: 8px; }
        .description { color: #666; font-size: 14px; margin-bottom: 15px; line-height: 1.4; }
        .price { color: #ff6b6b; font-size: 24px; font-weight: bold; }
    </style>
</head>
<body>
    <div class='container'>
        <header>
            <h1>🍛 Spice of India</h1>
            <p class='tagline'>Authentic Indian Cuisine</p>
        </header>
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
", "text/html"));

app.MapGet("/api/menu", () => menu);

app.Run();

record MenuItem(string Name, string Description, decimal Price, string Image);

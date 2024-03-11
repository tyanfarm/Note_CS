<div style="text-align: center; background-color: #FFBFBF; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
DOTNET NEW WEB
</div>

# Một số lệnh dotnet
-`dotnet run` để chạy server `localhost`
- `dotnet publish` để xuất ra dự án

# Cấu hình HTTPS khi bị warning
- `dotnet dev-certs https --clean`
- `dotnet dev-certs https --trust`

# Middleware & Pipeline
- ![alt](https://images.viblo.asia/d4de0491-220f-4a55-833e-41cfda531038.png)

- `Request` đến từ trình duyệt sẽ đi qua `Kestrel` web server rồi qua `pipeline` và quay trở lại khi xử lý xong để trả về `client`. Các thành phần đơn lẻ tạo nên pipeline này được gọi là `middleware`.

# IHost 
- DI: IServiceProvider (ServiceCollection)
- Logging (ILogging)
- Configuring
- IHostedService => StartAsync: Run HTTP Server (Kestrel http)

- Các bước:
    + Tạo IHostBuilder
    + Cấu hình, đăng ký các dịch vụ (gọi ConfigureWebHostDefaults)
    + IHostBuilder.Build() => Host (IHost)
    + Host.Run()

# Webpack
- Install: 
    + Trước tiên ta cần cài `Node.js` để sử dụng lệnh `npm`

    + Thiết lập môi trường để sử dụng
    
    + ![alt](https://i.pinimg.com/736x/8b/05/34/8b0534dd4096818839a9183e153cdbe6.jpg)

    + Lệnh cài `webpack`:
    + ```
        # tạo file package.json cho dự án
        npm init -y         

        # cài đặt Webpack                                
        npm i -D webpack webpack-cli  

        # cài đặt các gói để làm việc với SCSS
        npm i node-sass postcss-loader postcss-preset-env

        # cài đặt các gói để làm việc với SCSS, CSS
        npm i sass-loader css-loader cssnano

        # cài đặt các gói để làm việc với SCSS
        npm i mini-css-extract-plugin cross-env file-loader

        # cài đặt plugin copy file cho Webpack
        npm install copy-webpack-plugin 

        # package giám sát file  thay đổi
        npm install npm-watch                       
// const BundleAnalyzerPlugin = require('webpack-bundle-analyzer')
//     .BundleAnalyzerPlugin;
module.exports = {
    // configureWebpack: {
    //     plugins: [new BundleAnalyzerPlugin()]
    // },
    pages:{
        index:{
            entry:"src/main.js",
            title: "sicade"
        }
    }
};

let path = require('path');

module.exports = (env) => {
    const isPro = env && env.production;
    return {
        mode: isPro ? 'production' : 'development',
        devtool: isPro ? '' : 'source-maps',
        entry: {
            'app': './src/js/index.jsx'
        },
        output: {
          path: path.resolve(__dirname, 'dist'),
          filename: '[name].bundle.js',
          publicPath: "/dist/"
        },
        module: {
            rules: [
              {
                test: /\.(js|jsx)$/,
                exclude: /(node_modules|bower_components)/,
                loader: "babel-loader"
              }
            ]
          },
          resolve: { extensions: ["*", ".js", ".jsx"] },
      }
};
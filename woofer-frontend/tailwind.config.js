const colors = require('tailwindcss/colors');

module.exports = {
  purge: ['./src/**/*.{js,jsx,ts,tsx}', './public/index.html'],
  darkMode: false, // or 'media' or 'class'
  theme: {


    minHeight: {
      '0': '0',
      '16': '4rem',
      '32': '8rem',
      '64': '16rem',
      '72': '18rem',
      '80': '20rem',
      '128': '32rem',
      'full': '100%',
    },
    boxShadow: {
      white: '0px 0px 13px -1px rgba(255,255,255,1)',
      innerW: 'inset 0px 0px 13px -8px rgba(255,255,255,1)'
    },
    extend: {

      spacing: {
        '72': '18rem',
        '84': '21rem',
        '96': '24rem',
        'kilo': '36rem',
        'mega': '48rem'
      },

      colors: {
        transparent: 'transparent',
        current: 'currentColor',

        midnight: '#0A014F',
        primary: '#F6CACA',
        light: '#FAE8EB',
        success: '#43AA8B',
        danger: '#EF3054',
        dark: '#000a17',

      }


    },

  },
  variants: {
    extend: {},
  },
  plugins: [],
}

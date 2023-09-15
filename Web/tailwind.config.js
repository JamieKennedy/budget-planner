/* eslint-disable no-undef */
/** @type {import('tailwindcss').Config} */

export default {
    content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
    theme: {
        extend: {
            transitionProperty: {
                width: 'width',
            },
            colors: {
                dark: '#1E2430',
                darkLigher: '#263647',
                blueMain: '#19EAFD',
            },
        },
    },
    plugins: [require('@tailwindcss/forms'), require('tailwind-scrollbar')],
};

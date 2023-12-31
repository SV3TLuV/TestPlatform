/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./src/**/*.{js,jsx,ts,tsx}"],
    theme: {
        extend: {
            colors: {
            },
            screens: {
                "none": "0px",
                "xs": "480px",
                "sm": "640px",
                "md": "768px",
                "lg": "1024px",
                "xl": "1280px",
                "2xl": "1536px",
            },
        },
    },
    plugins: [],
}

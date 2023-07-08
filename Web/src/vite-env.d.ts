/// <reference types="vite/client" />

interface ImportMetaEnv {
    VITE_API_BASEURL: string;
}

interface ImportMeta {
    readonly env: ImportMetaEnv;
}

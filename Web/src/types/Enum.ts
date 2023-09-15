import { z } from "zod";

export type EWidgetState = z.infer<typeof WidgetState>;
export const WidgetState = z.enum(["Loading", "Loaded", "Errored"]);

export type EFormState = z.infer<typeof FormState>;
export const FormState = z.enum(["Pending", "Errored", "Default"]);

export type EAuthState = z.infer<typeof AuthState>;
export const AuthState = z.enum(["Success", "Failure", "Pending"]);

export type EPageState = z.infer<typeof PageState>;
export const PageState = z.enum(["Loading", "Loaded", "AuthError"]);

export type ETrendDirection = z.infer<typeof TrendDirection>;
export const TrendDirection = z.enum(["Up", "Down", "Level"]);

export type ETheme = z.infer<typeof Theme>;
export const Theme = z.enum(["Light", "Dark"]);

import { z } from 'zod';

export type TAccount = z.infer<typeof AccountSchema>;
export const AccountSchema = z.object({
    id: z.string(),
    userId: z.string(),
    name: z.string(),
    colourHex: z.string(),
    balance: z.number(),
    created: z.coerce.date(),
    lastModified: z.coerce.date(),
});

export type TCreateAccount = z.infer<typeof CreateAccountSchema>;
export const CreateAccountSchema = z.object({
    userId: z.string(),
    name: z.string(),
    colourHex: z.string().nullish(),
    balance: z.number(),
});

export type TUpdateAccount = z.infer<typeof UpdateAccountSchema>;
export const UpdateAccountSchema = z.object({
    accountId: z.string(),
    userId: z.string(),
    name: z.string().nullish(),
    colourHex: z.string().nullish(),
    balance: z.number().nullish(),
});

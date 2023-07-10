import { z } from "zod";

export type TUser = z.infer<typeof UserSchema>;

const UserSchema = z.object({
    id: z.string().uuid(),
    userName: z.string(),
    email: z.string().email(),
    emailConfirmed: z.boolean(),
    phoneNumber: z.string(),
    phoneNumberConfirmed: z.boolean(),
    twoFactorEnabled: z.boolean(),
});

import { z } from "zod";

export type AuthorizeRequest = z.infer<typeof AuthorizeRequestSchema>;
export type ErrorResponse = z.infer<typeof ErrorResponseSchema>;
export type TokenPair = z.infer<typeof TokenPairSchema>;
export type TokenPayload = z.infer<typeof TokenPayloadSchema>;

const AuthorizeRequestSchema = z.object({
    email: z.string().email(),
    password: z.string(),
    keepLoggedIn: z.boolean(),
});

export const ErrorResponseSchema = z.object({
    StatusCode: z.number(),
    Message: z.string(),
});

const TokenPairSchema = z.object({
    RefreshToken: z.string(),
    AccessToken: z.string(),
});

const TokenPayloadSchema = z.object({
    aud: z.string(),
    customerGroupId: z.number(),
    exp: z.number(),
    iat: z.number(),
    iss: z.number(),
    memberId: z.number(),
    nbf: z.number(),
    siteId: z.number(),
    userId: z.number(),
});

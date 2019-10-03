SET IDENTITY_INSERT [dbo].[Aluno] ON
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (1, N'Silvio Gonçalves Neto', N'1994-08-26', NULL, 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (2, N'Edna Gonçalves', N'1966-07-18', NULL, 1, N'Professor', 2)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (3, N'Elias Mário Galvão', N'1951-07-09', NULL, 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (4, N'Raimunda Brenda Pires', N'1954-03-20', NULL, 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (5, N'Filipe Tiago Ferreira', N'1995-10-03', N'(92) 99159-1907', 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (6, N'Fernando Juan Nogueira', N'1970-04-12', N'(48) 99573-9395', 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (7, N'Yuri Rodrigo Farias', N'1969-10-25', N'(79) 99732-2414', 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (8, N'Maria Elza Heloisa Drumond', N'1953-08-10', NULL, 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (9, N'Letícia Renata Nunes', N'2000-11-24', N'(11) 99958-9646', 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (10, N'Renata Silvana Maya Aragão', N'1958-07-09', NULL, 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (11, N'Yago Iago Drumond', N'1992-06-09', N'(79) 98767-4427', 1, N'Aluno', NULL)
INSERT INTO [dbo].[Aluno] ([Id], [Nome], [DataNascimento], [Telefone], [CongregacaoId], [Discriminator], [UsuarioId]) VALUES (12, N'Erick Benjamin Fernando da Cunha', N'1974-06-26', N'(68) 99230-6741', 1, N'Aluno', NULL)
SET IDENTITY_INSERT [dbo].[Aluno] OFF

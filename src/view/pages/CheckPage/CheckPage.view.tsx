import {Box, Button, Checkbox, MenuItem, Select, Stack, TextField, Typography} from "@mui/material";
import {BackButton} from "../../components/BackButton/BackButton.tsx";
import type {useCheckPage} from "./CheckPage.state.ts";
import {useState} from "react";

export const CheckPageView = ({
                                  current,
                                  words,
                                  translations,
                                  checkWord,
                              }: ReturnType<typeof useCheckPage>) => {
    const [translation, setTranslation] = useState<string>("");

    return (
        <Box sx={{p: 3, mb: 2}}>
            <Stack direction="row" sx={{gap: 2, mb: 3}}>
                <BackButton/>
                <Typography variant="h3" sx={{color: "#364963"}}>
                    Проверка знаний
                </Typography>
            </Stack>

            <Typography sx={{mb: 2, color: "#4e4e65"}} fontWeight={"700"}>
                Слово: {current + 1} из {words.length}
            </Typography>

            <Stack
                sx={{
                    backgroundColor: "#ffffff",
                    p: 3,
                    borderRadius: 1,
                    border: "1px solid #ededed"
                }}
            >
                <Stack
                    sx={{
                        maxWidth: "650px",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        alignItems: "center",
                        mb: 4
                    }}
                >
                    <Typography variant="body1" sx={{color: "#4e4e65"}}>
                        Слово на русском языке
                    </Typography>
                    <TextField
                        fullWidth
                        variant="outlined"
                        value={words[current] ?? ""}
                        sx={{maxWidth: "250px"}}
                        InputProps={{readOnly: true}}
                    />
                </Stack>

                <Stack
                    sx={{
                        maxWidth: "650px",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        alignItems: "center"
                    }}
                >
                    <Typography variant="body1">Перевод на английский язык</Typography>
                    <Select
                        sx={{width: "250px"}}
                        value={translation}
                        displayEmpty
                        onChange={(e) => setTranslation(String(e.target.value))}
                        renderValue={(val) =>
                            val ? val : <Typography color="text.secondary">Не выбрано</Typography>
                        }
                    >
                        {translations.map((opt, i) => (
                            <MenuItem key={i} value={opt}>
                                <Checkbox edge="start" checked={translation === opt} tabIndex={-1} disableRipple/>
                                {opt}
                            </MenuItem>
                        ))}
                    </Select>
                </Stack>
            </Stack>
            <Button
                variant="contained"
                onClick={() => {
                    checkWord(words[current], translation);
                    setTranslation("");
                }}
                sx={{display: "flex", mt: 2}}
                disabled={!translation}
            >
                Проверить
            </Button>
        </Box>
    );
};

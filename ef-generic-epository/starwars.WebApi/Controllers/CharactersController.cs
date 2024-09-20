﻿using Microsoft.AspNetCore.Mvc;
using starwars.Domain;
using starwars.IBusinessLogic;
using starwars.WebApi.Dtos;
using System.Collections.Generic;
using starwars.Exceptions.BusinessLogicExceptions;
using starwars.WebApi.Filters;

namespace starwars.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CharactersController : ControllerBase
{
    private ICharacterService _characterService;

    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet]
    public IActionResult GetCharacters()
    {
        return Ok(_characterService.GetCharacters().Select(c => new CharacterDTO(c)).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetCharacterById([FromRoute] int id)
    {
        CharacterDTO characterResponseModel = new CharacterDTO(_characterService.GetCharacterById(id));
        return Ok(characterResponseModel);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCharacterById([FromRoute] int id)
    {
        var character = _characterService.DeleteCharacter(id);
        return character == null ? NotFound() : Ok();
    }

    [HttpPost]
    public IActionResult InsertCharacter([FromBody] CharacterCreateModel newCharacter)
    {
        var character = _characterService.InsertCharacter(newCharacter.ToEntity());
        return character == null ? NotFound() : Ok(character);
    }

}

